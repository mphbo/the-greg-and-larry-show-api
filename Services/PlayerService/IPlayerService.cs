using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using the_greg_and_larry_show_api.Dtos.Player;

namespace the_greg_and_larry_show_api.Services
{
    public interface IPlayerService
    {
        Task<ServiceResponse<List<GetPlayerDto>>> GetAllPlayers();
        Task<ServiceResponse<GetPlayerDto>> GetPlayerById(int id);
        Task<ServiceResponse<List<GetPlayerDto>>> AddPlayer(AddPlayerDto newPlayer);
        Task<ServiceResponse<GetPlayerDto>> UpdatePlayer(UpdatePlayerDto player);
        Task<ServiceResponse<List<GetPlayerDto>>> DeletePlayer(int id);
    }
}