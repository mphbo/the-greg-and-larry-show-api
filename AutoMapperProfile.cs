using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using the_greg_and_larry_show_api.Dtos.User;
using the_greg_and_larry_show_api.Dtos.Round;

namespace the_greg_and_larry_show_api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<GetUserDto, User>();
            CreateMap<UserRegisterDto, User>();
            CreateMap<Round, GetRoundDto>();
            CreateMap<AddRoundDto, Round>();
        }
    }
}