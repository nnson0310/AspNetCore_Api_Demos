using AspCoreWebAPIDemos.DataStorages;
using AspCoreWebAPIDemos.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreWebAPIDemos.Controllers
{
    [Route("api/city/{cityId}/district")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<District>> GetAllDistricts(int cityId)
        {
            City? city = new CitiesDataStore().Cities.FirstOrDefault(city => city.Id == cityId);

            if (city is null)
            {
                return NotFound();
            }

            IEnumerable<District>? districts = city.Districts;

            if (districts is null)
            {
                return NotFound();
            }

            return Ok(districts);
        }

        [HttpGet("{districtId}")]
        public ActionResult<District> GetDistrict(int cityId, int districtId)
        {
            City? city = new CitiesDataStore().Cities.FirstOrDefault(city => city.Id == cityId);

            if (city is null || city.Districts is null)
            {
                return NotFound();
            }

            District? district = city.Districts.FirstOrDefault(district => district.Id == districtId);

            if (district is null)
            {
                return NotFound();
            }

            return Ok(district);
        }
    }
}
