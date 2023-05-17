using AspCoreWebAPIDemos.DataStorages;
using AspCoreWebAPIDemos.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreWebAPIDemos.Controllers
{
    [Route("api/city")]
    [ApiController]
    public class CityController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<City>> GetAllCities()
        {
            CitiesDataStore dataStore = new CitiesDataStore();

            if (dataStore.Cities is null)
            {
                return NotFound();
            }

            return Ok(dataStore.Cities);
        }

        [HttpGet("/{id}")]
        public ActionResult<City> GetCity(int id)
        {
            City? city = new CitiesDataStore().Cities.FirstOrDefault(city => city.Id == id);

            if (city is null)
            {
                return NotFound();
            }

            return Ok(city);
        }
    }
}
