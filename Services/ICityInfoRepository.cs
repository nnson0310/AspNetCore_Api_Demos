using AspCoreWebAPIDemos.DBContexts;
using AspCoreWebAPIDemos.Entities;
using AspCoreWebAPIDemos.Models;

namespace AspCoreWebAPIDemos.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<CityEntity>> GetCitiesAsync();

        Task<IEnumerable<CityEntity>> GetCitiesAsync(string? name, string? queryString);

        Task<CityEntity?> GetCityAsync(int cityId, bool includeRate);

        Task<IEnumerable<RateEntity>> GetRatesAsync(int cityId);

        Task<RateEntity?> GetRateAsync(int cityId, int rateId);

        Task<bool> DoesCityExist(int cityId);

        Task AddRate(int cityId, RateEntity newRate);

        Task<bool> SaveChangesAsync();

        void DeleteRate(RateEntity rate);
    }
}
