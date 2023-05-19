using AspCoreWebAPIDemos.DataStorages;
using AspCoreWebAPIDemos.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspCoreWebAPIDemos.Controllers
{
    [Route("api/city/{cityId}/rate")]
    [ApiController]
    public class RateController : ControllerBase
    {
        [HttpGet("{rateId}", Name = "GetRateOfCity")]
        [Produces("application/json")]
        public ActionResult<Rate> SubmitRate(int cityId, int rateId)
        {
            var city = CitiesDataStore.CitiesData.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city is null)
            {
                return NotFound("There is no matching city");
            }

            var rate = city.PointRate!.FirstOrDefault(r => r.Id == rateId);

            if (rate is null)
            {
                return NotFound("There is no matching rate");
            }

            return Ok(rate);
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<Rate> SubmitRate(
            int cityId,
            [FromBody] RateForCreation rateForCreation)
        {
            var city = CitiesDataStore.CitiesData.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city is null)
            {
                return NotFound("There is no matching city");
            }

            var lastestRateId = city.PointRate!.Max(p => p.Id);

            var finalSubmittedRate = new Rate()
            {
                Id = ++lastestRateId,
                GuestName = rateForCreation.GuestName,
                Point = rateForCreation.Point,
            };

            city.PointRate!.Add(finalSubmittedRate);

            return CreatedAtRoute(
                "GetRateOfCity",
                new
                {
                    cityId = cityId,
                    rateId = finalSubmittedRate.Id,
                },
                finalSubmittedRate);
        }
    }
}
