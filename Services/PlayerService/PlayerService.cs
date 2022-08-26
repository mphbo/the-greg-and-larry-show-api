using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using the_greg_and_larry_show_api.Dtos.Player;
using the_greg_and_larry_show_api.Models;

namespace the_greg_and_larry_show_api.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private static List<Player> players = new List<Player> {
            new Player(),
            new Player {Name = "Larry", Id = 1}
        };
        private readonly IMapper _mapper;

        public PlayerService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetPlayerDto>>> AddPlayer(AddPlayerDto newPlayer)
        {
            var serviceResponse = new ServiceResponse<List<GetPlayerDto>>();
            Player player = _mapper.Map<Player>(newPlayer);
            player.Id = players.Max(p => p.Id) + 1;
            players.Add(player);
            serviceResponse.Data = players.Select(p => _mapper.Map<GetPlayerDto>(p)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPlayerDto>>> DeletePlayer(int id)
        {
            ServiceResponse<List<GetPlayerDto>> response = new ServiceResponse<List<GetPlayerDto>>();
            try
            {
                Player player = players.First(p => p.Id == id);
                players.Remove(player);
                response.Data = players.Select(p => _mapper.Map<GetPlayerDto>(p)).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        public async Task<ServiceResponse<List<GetPlayerDto>>> GetAllPlayers()
        {
            return new ServiceResponse<List<GetPlayerDto>>
            {
                Data = players.Select(p => _mapper.Map<GetPlayerDto>(p)).ToList()
            };
        }

        public async Task<ServiceResponse<GetPlayerDto>> GetPlayerById(int id)
        {
            var serviceResponse = new ServiceResponse<GetPlayerDto>();
            var player = players.FirstOrDefault(p => p.Id == id);
            serviceResponse.Data = _mapper.Map<GetPlayerDto>(player);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPlayerDto>> UpdatePlayer(UpdatePlayerDto updatedPlayer)
        {
            ServiceResponse<GetPlayerDto> response = new ServiceResponse<GetPlayerDto>();
            try
            {
                Player player = players.FirstOrDefault(p => p.Id == updatedPlayer.Id);

                player.Name = updatedPlayer.Name;
                player.Email = updatedPlayer.Email;

                response.Data = _mapper.Map<GetPlayerDto>(player);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}