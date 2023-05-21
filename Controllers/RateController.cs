using AspCoreWebAPIDemos.DataStorages;
using AspCoreWebAPIDemos.Models;
using Microsoft.AspNetCore.JsonPatch;
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

            var updateRate = city.PointRate!.FirstOrDefault(r => r.Id == rateId);
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

            var updateRate = city.PointRate!.FirstOrDefault(r => r.Id == rateId);
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

            var updateRate = city.PointRate!.FirstOrDefault(r => r.Id == rateId);
            if (updateRate is null)
            {
                return NotFound("There is no matching rate");
            }

            city.PointRate!.Remove(updateRate);

            return NoContent();
        }
    }
}
