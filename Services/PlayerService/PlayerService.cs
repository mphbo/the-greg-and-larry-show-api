using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using the_greg_and_larry_show_api.Models;

namespace the_greg_and_larry_show_api.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private static List<Player> players = new List<Player> {
            new Player(),
            new Player {Name = "Larry", Id = 1}
        };

        public async Task<ServiceResponse<List<Player>>> AddPlayer(Player newPlayer)
        {
            var serviceResponse = new ServiceResponse<List<Player>>();
            players.Add(newPlayer);
            serviceResponse.Data = players;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Player>>> GetAllPlayers()
        {
            return new ServiceResponse<List<Player>> { Data = players };
        }

        public async Task<ServiceResponse<Player>> GetPlayerById(int id)
        {
            var serviceResponse = new ServiceResponse<Player>();
            var player = players.FirstOrDefault(p => p.Id == id);
            serviceResponse.Data = player;
            return serviceResponse;
        }
    }
}