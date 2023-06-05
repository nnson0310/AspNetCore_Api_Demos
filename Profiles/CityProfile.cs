using AutoMapper;

namespace AspCoreWebAPIDemos.Profiles
{
    public class CityProfile: Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.CityEntity, Models.CityWithoutRate>();
        }
    }
}
