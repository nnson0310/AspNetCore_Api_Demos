using AspCoreWebAPIDemos.DataStorages;
using AspCoreWebAPIDemos.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreWebAPIDemos.Controllers
{
    [Route("api/city/{cityId}/rate")]
    [ApiController]
    public class RateController : ControllerBase
    {
        [HttpPost]
        public ActionResult<Rate> SubmitRate(
            int cityId, 
            [FromBody] RateForCreation rateForCreation)
        {
            var city = CitiesDataStore.CitiesData.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city is null)
            {
                return NotFound("There is no matching city");
            }

            var lastestRateId = CitiesDataStore.CitiesData.Cities.SelectMany(c => c.PointRate!).Max(p => p.Id);
        }
    }
}
