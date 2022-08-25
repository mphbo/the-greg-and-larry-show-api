using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace the_greg_and_larry_show_api.Services
{
    public interface IPlayerService
    {
        Task<ServiceResponse<List<Player>>> GetAllPlayers();
        Task<ServiceResponse<Player>> GetPlayerById(int id);
        Task<ServiceResponse<List<Player>>> AddPlayer(Player newPlayer);
    }
}