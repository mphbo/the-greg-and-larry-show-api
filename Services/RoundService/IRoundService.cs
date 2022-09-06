using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using the_greg_and_larry_show_api.Dtos.Round;

namespace the_greg_and_larry_show_api.Services.RoundService
{
    public interface IRoundService
    {
        Task<ServiceResponse<List<GetRoundDto>>> GetAllRounds();
        Task<ServiceResponse<GetRoundDto>> GetRoundById(int id);
        Task<ServiceResponse<List<GetRoundDto>>> AddRound();
        Task<ServiceResponse<GetRoundDto>> UpdateRound(UpdateRoundDto updatedRound);
        Task<ServiceResponse<List<GetRoundDto>>> DeleteRound(int id);
    }
}