using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using the_greg_and_larry_show_api.Dtos.Player;
using the_greg_and_larry_show_api.Dtos.Round;

namespace the_greg_and_larry_show_api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Player, GetPlayerDto>();
            CreateMap<AddPlayerDto, Player>();
            CreateMap<Round, GetRoundDto>();
            CreateMap<AddRoundDto, Round>();
        }
    }
}