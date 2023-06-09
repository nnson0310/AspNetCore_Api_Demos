using AspCoreWebAPIDemos.DBContexts;
using AspCoreWebAPIDemos.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspCoreWebAPIDemos.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityContext _cityContext;
        private readonly ILogger<CityInfoRepository> _logger;

        public CityInfoRepository(CityContext cityContext, ILogger<CityInfoRepository> logger)
        {
            _cityContext = cityContext ?? throw new ArgumentNullException(nameof(cityContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<CityEntity>> GetCitiesAsync()
        {
            return await _cityContext.City.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<IEnumerable<CityEntity>> GetCitiesAsync(string? name, string? queryString)
        {
            if (string.IsNullOrEmpty(name) && string.IsNullOrWhiteSpace(queryString))
            {
                return await GetCitiesAsync();
            }

            var cities = _cityContext.City as IQueryable<CityEntity>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                cities = cities.Where(c => c.Name == name.Trim());
            }

            if (!string.IsNullOrWhiteSpace(queryString))
            {
                cities = cities.Where(c => c.Name.Contains(queryString.Trim()) || (c.Description != null && c.Description.Contains(queryString.Trim())));
            }

            return await cities.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<CityEntity?> GetCityAsync(int cityId, bool includeRate)
        {
            if (includeRate)
            {
                return await _cityContext.City.Include(c => c.Rates).Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }
            return await _cityContext.City.Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<bool> DoesCityExist(int cityId)
        {
            return await _cityContext.City.AnyAsync(c => c.Id == cityId);
        }

        public async Task<RateEntity?> GetRateAsync(int cityId, int rateId)
        {
            return await _cityContext.Rate.Where(r => r.CityId == cityId && r.Id == rateId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RateEntity>> GetRatesAsync(int cityId)
        {
            return await _cityContext.Rate.Where(r => r.CityId == cityId).ToListAsync();
        }

        public async Task AddRate(int cityId, RateEntity newRate)
        {
            var city = await GetCityAsync(cityId, true);
            if (city != null && city.Rates != null)
            {
                city.Rates.Add(newRate);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _cityContext.SaveChangesAsync() >= 0;
        }

        public void DeleteRate(RateEntity rate)
        {
            _cityContext.Rate.Remove(rate);
        }
    }
}
