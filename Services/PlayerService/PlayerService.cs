using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace the_greg_and_larry_show_api.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private static List<Player> players = new List<Player> {
            new Player(),
            new Player {Name = "Larry", Id = 1}
        };

        public List<Player> AddPlayer(Player newPlayer)
        {
            players.Add(newPlayer);
            return players;
        }

        public List<Player> GetAllPlayers()
        {
            return players;
        }

        public Player GetPlayerById(int id)
        {
            return players.FirstOrDefault(p => p.Id == id);
        }
    }
}