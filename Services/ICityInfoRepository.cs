using AspCoreWebAPIDemos.Entities;

namespace AspCoreWebAPIDemos.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();

        Task<City?> GetCityAsync(int cityId, bool includeRate);

        Task<IEnumerable<Rate>> GetRatesAsync(int cityId);

        Task<Rate?> GetRateAsync(int cityId, int rateId);
    }
}
