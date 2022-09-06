using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoundService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetRoundDto>>> AddRound()
        {
            var response = new ServiceResponse<List<GetRoundDto>>();
            Round round = _mapper.Map<Round>(new AddRoundDto { UserId = GetUserId() });

            round.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            _context.Rounds.Add(round);
            await _context.SaveChangesAsync();
            response.Data = await _context.Rounds.Where(r => r.User.Id == GetUserId()).Select(r => _mapper.Map<GetRoundDto>(r)).ToListAsync();
            return response;

        }

        public async Task<ServiceResponse<List<GetRoundDto>>> DeleteRound(int id)
        {
            var response = new ServiceResponse<List<GetRoundDto>>();

            try
            {
                Round round = await _context.Rounds.FirstOrDefaultAsync(r => r.Id == id && r.User.Id == GetUserId());
                if (round != null)
                {
                    _context.Rounds.Remove(round);
                    await _context.SaveChangesAsync();
                    response.Data = await _context.Rounds.Where(r => r.User.Id == GetUserId()).Select(r => _mapper.Map<GetRoundDto>(r)).ToListAsync();
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

        public async Task<ServiceResponse<List<GetRoundDto>>> GetAllRounds()
        {
            var response = new ServiceResponse<List<GetRoundDto>>();
            var dbRounds = await _context.Rounds.Where(r => r.User.Id == GetUserId()).ToListAsync();
            response.Data = dbRounds.Select(p => _mapper.Map<GetRoundDto>(p)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetRoundDto>> GetRoundById(int id)
        {
            var response = new ServiceResponse<GetRoundDto>();
            var dbRound = await _context.Rounds.Where(r => r.User.Id == GetUserId()).FirstOrDefaultAsync(r => r.Id == id);
            response.Data = _mapper.Map<GetRoundDto>(dbRound);
            return response;
        }

        public async Task<ServiceResponse<GetRoundDto>> UpdateRound(UpdateRoundDto updatedRound)
        {
            ServiceResponse<GetRoundDto> response = new ServiceResponse<GetRoundDto>();
            try
            {
                var dbRound = await _context.Rounds.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == updatedRound.Id);

                if (dbRound.User.Id == GetUserId())
                {
                    dbRound.Score = updatedRound.Score;
                    dbRound.Level = updatedRound.Level;
                    dbRound.IsDeleted = updatedRound.IsDeleted;
                    dbRound.IsSaved = true;
                }
                else
                {
                    response.Success = false;
                    response.Message = "Saved game not found.";
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