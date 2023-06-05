using AspCoreWebAPIDemos.DataStorages;
using AspCoreWebAPIDemos.Models;
using AspCoreWebAPIDemos.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreWebAPIDemos.Controllers
{
    [Route("api/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private readonly ICityInfoRepository _cityInfoRepository;

        public CityController(ILogger<CityController> logger, ICityInfoRepository cityInfoRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        }

        [HttpGet]
        public ActionResult<List<City>> GetAllCities()
        {
            //CitiesDataStore dataStore = new();

            //if (dataStore.Cities is null)
            //{
            //    return NotFound();
            //}

            //return Ok(dataStore.Cities);

            var cities = _cityInfoRepository.GetCitiesAsync();
            if (cities is null)
            {
                return NotFound();
            }
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public ActionResult<City> GetCity(int id)
        {
            City? city = new CitiesDataStore().Cities.FirstOrDefault(city => city.Id == id);

            if (city is null)
            {
                _logger.LogError("Can not get city info with city id = {id}", id);
                return NotFound();
            }

            return Ok(city);
        }
    }
}
