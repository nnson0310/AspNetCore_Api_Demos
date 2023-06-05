using AspCoreWebAPIDemos.DBContexts;
using AspCoreWebAPIDemos.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspCoreWebAPIDemos.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityContext _cityContext;

        public CityInfoRepository(CityContext cityContext)
        {
            _cityContext = cityContext ?? throw new ArgumentNullException(nameof(cityContext));
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _cityContext.City.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool includeRate)
        {
            if (includeRate)
            {
                return await _cityContext.City.Include(c => c.Rates).Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }
            return await _cityContext.City.Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<Rate?> GetRateAsync(int cityId, int rateId)
        {
            return await _cityContext.Rate.Where(r => r.CityId == cityId && r.Id == rateId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Rate>> GetRatesAsync(int cityId)
        {
            return await _cityContext.Rate.Where(r => r.CityId == cityId).ToListAsync();
        }
    }
}
