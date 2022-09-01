using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using the_greg_and_larry_show_api.Data;
using the_greg_and_larry_show_api.Dtos.Player;
using the_greg_and_larry_show_api.Models;

namespace the_greg_and_larry_show_api.Services.PlayerService
{
    public class PlayerService : IPlayerService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public PlayerService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetPlayerDto>>> AddPlayer(AddPlayerDto newPlayer)
        {
            var serviceResponse = new ServiceResponse<List<GetPlayerDto>>();
            Player player = _mapper.Map<Player>(newPlayer);
            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.Players.Select(p => _mapper.Map<GetPlayerDto>(p)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPlayerDto>>> DeletePlayer(int id)
        {
            ServiceResponse<List<GetPlayerDto>> response = new ServiceResponse<List<GetPlayerDto>>();
            try
            {
                var dbPlayer = await _context.Players.FirstAsync(p => p.Id == id);
                _context.Players.Remove(dbPlayer);
                await _context.SaveChangesAsync();
                response.Data = _context.Players.Select(p => _mapper.Map<GetPlayerDto>(p)).ToList();
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
            var response = new ServiceResponse<List<GetPlayerDto>>();
            var dbPlayers = await _context.Players.ToListAsync();
            response.Data = dbPlayers.Select(p => _mapper.Map<GetPlayerDto>(p)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetPlayerDto>> GetPlayerById(int id)
        {
            var serviceResponse = new ServiceResponse<GetPlayerDto>();
            var dbPlayer = await _context.Players.Include(p => p.Rounds).FirstOrDefaultAsync(p => p.Id == id);
            serviceResponse.Data = _mapper.Map<GetPlayerDto>(dbPlayer);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPlayerDto>> UpdatePlayer(UpdatePlayerDto updatedPlayer)
        {
            ServiceResponse<GetPlayerDto> response = new ServiceResponse<GetPlayerDto>();
            try
            {
                var dbPlayer = await _context.Players.FirstOrDefaultAsync(p => p.Id == updatedPlayer.Id);

                if (dbPlayer != null)
                {
                    dbPlayer.FirstName = updatedPlayer.FirstName;
                    dbPlayer.LastName = updatedPlayer.LastName;
                    dbPlayer.Email = updatedPlayer.Email;
                }

                await _context.SaveChangesAsync();


                response.Data = _mapper.Map<GetPlayerDto>(dbPlayer);
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