using AspCoreWebAPIDemos.DataStorages;
using AspCoreWebAPIDemos.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreWebAPIDemos.Controllers
{
    [Route("api/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;

        public CityController(ILogger<CityController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public ActionResult<List<City>> GetAllCities()
        {
            CitiesDataStore dataStore = new();

            if (dataStore.Cities is null)
            {
                return NotFound();
            }

            return Ok(dataStore.Cities);
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
