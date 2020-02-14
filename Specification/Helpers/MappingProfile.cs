using AutoMapper;
using DevItUp.Grain.API.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevItUp.Grain.API.Specification.Helpers
{
    public static class MappingProfile
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });

            return config;
        }
    }
}
