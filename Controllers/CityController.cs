using AspCoreWebAPIDemos.Models;
using AspCoreWebAPIDemos.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AspCoreWebAPIDemos.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/city")]
    [Authorize]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        private readonly int maxedPageSize = 10;

        public CityController(
            ILogger<CityController> logger,
            ICityInfoRepository cityInfoRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Get all information of cities
        /// </summary>
        /// <param name="name">Name of city</param>
        /// <param name="queryString">Query string to search exact city</param>
        /// <param name="pageNumber">Number of page (used in pagination)</param>
        /// <param name="pageSize">Result per page</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<CityWithoutRate>>> GetAllCities(
            [FromQuery] string? name,
            [FromQuery] string? queryString,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5)
        {
            //CitiesDataStore dataStore = new();

            //if (dataStore.Cities is null)
            //{
            //    return NotFound();
            //}

            //return Ok(dataStore.Cities);

            if (pageSize > maxedPageSize)
            {
                pageSize = maxedPageSize;
            }

            var (cities, paginationMetaData) = await _cityInfoRepository.GetCitiesAsync(name, queryString, pageNumber, pageSize);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));

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

        [HttpGet("bangkok/details")]
        [Authorize(Policy = "MustBeFromBangkok")]
        public ActionResult GetBangkokCityDetail()
        {
            string bangkokDetails = "{" +
                "\"city\": \"Bangkok\", " +
                "\"population\": \"10.69 million (2023)\", " +
                "\"area_cover\": \"1,568 sq km\", " +
                "\"tourism\": \"The Grand Palace\", " +
                "\"transportation\": \"bus, taxi (grab), MRT, sky train\"}";

            BangkokDetails? details = JsonSerializer.Deserialize<BangkokDetails>(bangkokDetails);

            return Ok(details);
        }

        private class BangkokDetails
        {
            [JsonPropertyName("city")]
            public string? City { get; set; }

            [JsonPropertyName("population")]
            public string? Population { get; set; }

            [JsonPropertyName("area_cover")]
            public string? AreaCover { get; set; }

            [JsonPropertyName("tourism")]
            public string? Tourism { get; set; }

            [JsonPropertyName("transportation")]
            public string? Transportation { get; set; }
        }
    }
}
