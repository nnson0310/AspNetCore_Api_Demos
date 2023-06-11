using AspCoreWebAPIDemos.Entities;
using AspCoreWebAPIDemos.Models;

namespace AspCoreWebAPIDemos.Services
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<CityEntity>> GetCitiesAsync();

        Task<(IEnumerable<CityEntity>, PaginationMetaData)> GetCitiesAsync(string? name, string? queryString, int pageNumber, int pageSize);

        Task<CityEntity?> GetCityAsync(int cityId, bool includeRate);

        Task<IEnumerable<RateEntity>> GetRatesAsync(int cityId);

        Task<RateEntity?> GetRateAsync(int cityId, int rateId);

        Task<UserEntity?> GetUserCredentials(string username, string password);  

        Task<bool> DoesCityExist(int cityId);

        Task AddRate(int cityId, RateEntity newRate);

        Task<bool> SaveChangesAsync();

        void DeleteRate(RateEntity rate);

        Task<bool> DoesCityNameMatchCityId(int? cityId, string? cityName);
    }
}
