using AspCoreWebAPIDemos.DBContexts;
using AspCoreWebAPIDemos.Entities;

namespace AspCoreWebAPIDemos.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<CityEntity>> GetCitiesAsync();

        Task<CityEntity?> GetCityAsync(int cityId, bool includeRate);

        Task<IEnumerable<RateEntity>> GetRatesAsync(int cityId);

        Task<RateEntity?> GetRateAsync(int cityId, int rateId);

        Task<bool> DoesCityExist(int cityId);
    }
}
