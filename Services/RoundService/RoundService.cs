using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using the_greg_and_larry_show_api.Data;
using the_greg_and_larry_show_api.Dtos.Round;

namespace the_greg_and_larry_show_api.Services.RoundService
{
    public class RoundService : IRoundService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public RoundService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetRoundDto>>> AddRound(AddRoundDto newRound)
        {
            var response = new ServiceResponse<List<GetRoundDto>>();
            Round round = _mapper.Map<Round>(newRound);

            round.User = await _context.Users.FirstOrDefaultAsync(p => p.Id == newRound.UserId);

            _context.Rounds.Add(round);
            await _context.SaveChangesAsync();
            response.Data = await _context.Rounds.Where(r => r.User.Id == newRound.UserId).Select(p => _mapper.Map<GetRoundDto>(p)).ToListAsync();
            return response;

        }

        public async Task<ServiceResponse<int>> DeleteRound(int id)
        {
            var response = new ServiceResponse<int>();

            try
            {
                Round round = await _context.Rounds.FirstOrDefaultAsync(r => r.Id == id);
                if (round != null)
                {
                    _context.Rounds.Remove(round);
                    await _context.SaveChangesAsync();
                    response.Data = round.Id;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Saved game not found";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetRoundDto>>> GetAllRounds(int userId)
        {
            var response = new ServiceResponse<List<GetRoundDto>>();
            var dbRounds = await _context.Rounds.Where(r => r.User.Id == userId).ToListAsync();
            response.Data = dbRounds.Select(p => _mapper.Map<GetRoundDto>(p)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetRoundDto>> GetRoundById(int id)
        {
            var response = new ServiceResponse<GetRoundDto>();
            var dbRound = await _context.Rounds.FirstOrDefaultAsync(r => r.Id == id);
            response.Data = _mapper.Map<GetRoundDto>(dbRound);
            return response;
        }

        public async Task<ServiceResponse<GetRoundDto>> UpdateRound(UpdateRoundDto updatedRound)
        {
            ServiceResponse<GetRoundDto> response = new ServiceResponse<GetRoundDto>();
            try
            {
                var dbRound = await _context.Rounds.FirstOrDefaultAsync(r => r.Id == updatedRound.Id);

                if (dbRound != null)
                {
                    dbRound.Score = updatedRound.Score;
                    dbRound.Level = updatedRound.Level;
                    dbRound.IsDeleted = updatedRound.IsDeleted;
                    dbRound.IsSaved = true;
                }

                await _context.SaveChangesAsync();


                response.Data = _mapper.Map<GetRoundDto>(dbRound);
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