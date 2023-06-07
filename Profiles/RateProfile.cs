using AutoMapper;

namespace AspCoreWebAPIDemos.Profiles
{
    public class RateProfile : Profile
    {
        public RateProfile()
        {
            CreateMap<Entities.RateEntity, Models.Rate>();
            CreateMap<Models.RateForCreation, Entities.RateEntity>();
            CreateMap<Models.RateForUpdate, Entities.RateEntity>();
            CreateMap<Entities.RateEntity, Models.RateForUpdate>();
        }
    }
}
