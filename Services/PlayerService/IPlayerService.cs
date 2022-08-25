using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace the_greg_and_larry_show_api.Services
{
    public interface IPlayerService
    {
        List<Player> GetAllPlayers();
        Player GetPlayerById(int id);
        List<Player> AddPlayer(Player newPlayer);
    }
}