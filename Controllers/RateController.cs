using AspCoreWebAPIDemos.Entities;
using AspCoreWebAPIDemos.Models;
using AspCoreWebAPIDemos.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreWebAPIDemos.Controllers
{
    [Route("api/city/{cityId}/rate")]
    [Authorize]
    [ApiController]
    public class RateController : ControllerBase
    {
        private ILogger<RateController> _logger;
        private IMailService _mailService;
        private ICityInfoRepository _cityInfoRepository;
        private IMapper _mapper;

        public RateController(
            ILogger<RateController> logger,
            IMailService mailService,
            IMapper mapper,
            ICityInfoRepository cityInfoRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("{rateId}", Name = "GetRateOfCity")]
        [Produces("application/json")]
        public async Task<IActionResult> GetRateOfCity(int cityId, int rateId)
        {
            var cityName = User.Claims.FirstOrDefault(u => u.Type == "city")?.Value;

            if (!await _cityInfoRepository.DoesCityNameMatchCityId(cityId, cityName))
            {
                return Forbid();
            }

            if (!await _cityInfoRepository.DoesCityExist(cityId))
            {
                return NotFound($"Can not find city with id = {cityId}");
            }

            var rate = await _cityInfoRepository.GetRateAsync(cityId, rateId);

            if (rate is null)
            {
                return NotFound($"Can not find rate with id = {rateId}");
            }

            return Ok(_mapper.Map<Rate>(rate));
        }

        [HttpGet("", Name = "GetAllRatesOfCity")]
        [Produces("application/json")]
        public async Task<ActionResult<List<Rate>>> GetAllRatesOfCity(int cityId)
        {
            var cityName = User.Claims.FirstOrDefault(u => u.Type == "city")?.Value;

            if (!await _cityInfoRepository.DoesCityNameMatchCityId(cityId, cityName))
            {
                return Forbid();
            }

            if (!await _cityInfoRepository.DoesCityExist(cityId))
            {
                return NotFound($"Can not find city with id = {cityId}");
            }

            var rates = await _cityInfoRepository.GetRatesAsync(cityId);

            if (rates is null)
            {
                return NotFound("This city has no rates yet");
            }

            return Ok(_mapper.Map<List<Rate>>(rates));
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<Rate>> CreateNewRate(
            int cityId,
            [FromBody] RateForCreation newRate)
        {
            if (!await _cityInfoRepository.DoesCityExist(cityId))
            {
                return NotFound($"Can not find city with id = {cityId}");
            }

            var lastestRate = _mapper.Map<RateEntity>(newRate);

            await _cityInfoRepository.AddRate(cityId, lastestRate);

            await _cityInfoRepository.SaveChangesAsync();

            var submittedLastestRate = _mapper.Map<Rate>(lastestRate);

            return CreatedAtRoute(
                "GetRateOfCity",
                new
                {
                    cityId = cityId,
                    rateId = submittedLastestRate.Id,
                },
                submittedLastestRate);
        }

        [HttpPut("{rateId}")]
        [Produces("application/json")]
        public async Task<ActionResult<Rate>> UpdateRate(
            int cityId,
            int rateId,
            [FromBody] RateForUpdate rateForUpdate)
        {
            if (!await _cityInfoRepository.DoesCityExist(cityId))
            {
                return NotFound($"Can not find city with id = {cityId}");
            }

            var updateRate = await _cityInfoRepository.GetRateAsync(cityId, rateId);
            if (updateRate is null)
            {
                return NotFound("There is no matching rate");
            }

            _mapper.Map(rateForUpdate, updateRate);

            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{rateId}")]
        [Produces("application/json")]
        public async Task<ActionResult<Rate>> UpdateRatePartial(
            int cityId,
            int rateId,
            [FromBody] JsonPatchDocument<RateForUpdate> rateForUpdate)
        {
            if (!await _cityInfoRepository.DoesCityExist(cityId))
            {
                return NotFound($"Can not find city with id = {cityId}");
            }

            var rateNeedUpdate = await _cityInfoRepository.GetRateAsync(cityId, rateId);
            if (rateNeedUpdate is null)
            {
                return NotFound($"There is no rate matching with id = {rateId}");
            }

            var partialUpdateRate = _mapper.Map<RateForUpdate>(rateNeedUpdate);

            rateForUpdate.ApplyTo(partialUpdateRate, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(rateForUpdate))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(partialUpdateRate, rateNeedUpdate);

            await _cityInfoRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{rateId}")]
        [Produces("application/json")]
        public async Task<ActionResult<Rate>> DeleteRate(
            int cityId,
            int rateId)
        {
            if (!await _cityInfoRepository.DoesCityExist(cityId))
            {
                return NotFound($"Can not find city with id = {cityId}");
            }

            var deleteRate = await _cityInfoRepository.GetRateAsync(cityId, rateId);
            if (deleteRate is null)
            {
                return NotFound($"There is no rate matching with id = {rateId}");
            }

            _cityInfoRepository.DeleteRate(deleteRate);
            await _cityInfoRepository.SaveChangesAsync();

            _mailService.Send("Delete Rate", $"The rate with id = {rateId} of city id = {cityId} is deleted");

            return NoContent();
        }
    }
}
