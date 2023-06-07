﻿using AspCoreWebAPIDemos.DataStorages;
using AspCoreWebAPIDemos.Entities;
using AspCoreWebAPIDemos.Models;
using AspCoreWebAPIDemos.Services;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreWebAPIDemos.Controllers
{
    [Route("api/city/{cityId}/rate")]
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
        public ActionResult<Rate> UpdateRate(
            int cityId,
            int rateId,
            [FromBody] RateForUpdate rateForUpdate)
        {
            var city = CitiesDataStore.CitiesData.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city is null)
            {
                return NotFound("There is no matching city");
            }

            var updateRate = city.Rates!.FirstOrDefault(r => r.Id == rateId);
            if (updateRate is null)
            {
                return NotFound("There is no matching rate");
            }

            updateRate.GuestName = rateForUpdate.GuestName;
            updateRate.Point = rateForUpdate.Point;

            return NoContent();
        }

        [HttpPatch("{rateId}")]
        [Produces("application/json")]
        public ActionResult<Rate> UpdateRatePartial(
            int cityId,
            int rateId,
            [FromBody] JsonPatchDocument<RateForUpdate> rateForUpdate)
        {
            var city = CitiesDataStore.CitiesData.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city is null)
            {
                return NotFound("There is no matching city");
            }

            var updateRate = city.Rates!.FirstOrDefault(r => r.Id == rateId);
            if (updateRate is null)
            {
                return NotFound("There is no matching rate");
            }

            var partialRateUpdate = new RateForUpdate()
            {
                GuestName = updateRate.GuestName,
                Point = updateRate.Point
            };

            rateForUpdate.ApplyTo(partialRateUpdate, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(rateForUpdate))
            {
                return BadRequest(ModelState);
            }

            updateRate.GuestName = partialRateUpdate.GuestName;
            updateRate.Point = partialRateUpdate.Point;

            return NoContent();
        }

        [HttpDelete("{rateId}")]
        [Produces("application/json")]
        public ActionResult<Rate> DeleteRate(
            int cityId,
            int rateId)
        {
            var city = CitiesDataStore.CitiesData.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city is null)
            {
                return NotFound("There is no matching city");
            }

            var updateRate = city.Rates!.FirstOrDefault(r => r.Id == rateId);
            if (updateRate is null)
            {
                return NotFound("There is no matching rate");
            }

            city.Rates!.Remove(updateRate);
            _mailService.Send("Delete Rate", $"The rate with id = {rateId} of city id = {cityId} is deleted");

            return NoContent();
        }
    }
}
