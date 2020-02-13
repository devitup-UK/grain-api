using AutoMapper;
using DevItUp.Grain.API.Entities;
using DevItUp.Grain.API.Models.Users;

namespace DevItUp.Grain.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
        }
    }
}