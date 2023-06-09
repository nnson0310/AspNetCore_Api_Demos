﻿using AutoMapper;

namespace AspCoreWebAPIDemos.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.CityEntity, Models.CityWithoutRate>();
            CreateMap<Entities.CityEntity, Models.City>();
            CreateMap<Entities.UserEntity, Models.User>();
        }
    }
}
