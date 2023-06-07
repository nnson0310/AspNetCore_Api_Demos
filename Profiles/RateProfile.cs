using AutoMapper;

namespace AspCoreWebAPIDemos.Profiles
{
    public class RateProfile : Profile
    {
        public RateProfile()
        {
            CreateMap<Entities.RateEntity, Models.Rate>();
            CreateMap<Models.RateForCreation, Entities.RateEntity>();
        }
    }
}
