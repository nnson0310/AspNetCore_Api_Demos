using AspCoreWebAPIDemos.DataStorages;
using AspCoreWebAPIDemos.Entities;
using AspCoreWebAPIDemos.Models;
using AspCoreWebAPIDemos.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreWebAPIDemos.Controllers
{
    [Route("api/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CityController(
            ILogger<CityController> logger, 
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<List<CityWithoutRate>>> GetAllCities([FromQuery] string? name)
        {
            //CitiesDataStore dataStore = new();

            //if (dataStore.Cities is null)
            //{
            //    return NotFound();
            //}

            //return Ok(dataStore.Cities);

            var cities = await _cityInfoRepository.GetCitiesAsync(name);
            if (cities is null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<CityWithoutRate>>(cities));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id, bool includeRate = false)
        {
            var city = await _cityInfoRepository.GetCityAsync(id, includeRate);

            if (city is null)
            {
                _logger.LogError("Can not get city info with city id = {id}", id);
                return NotFound();
            }

            if (includeRate)
            {
                return Ok(_mapper.Map<City>(city));
            }

            return Ok(_mapper.Map<CityWithoutRate>(city));
        }
    }
}
