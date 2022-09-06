using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using the_greg_and_larry_show_api.Dtos.Round;
using the_greg_and_larry_show_api.Services.RoundService;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace the_greg_and_larry_show_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RoundController : ControllerBase
    {

        private readonly IRoundService _roundService;

        public RoundController(IRoundService roundService)
        {
            _roundService = roundService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetRoundDto>>>> Get()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            return Ok(await _roundService.GetAllRounds());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetRoundDto>>> GetSingle(int id, int userId)
        {
            return Ok(await _roundService.GetRoundById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetRoundDto>>>> AddRound()
        {
            return Ok(await _roundService.AddRound());
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetRoundDto>>> UpdateRound(UpdateRoundDto updatedRound)
        {
            var response = await _roundService.UpdateRound(updatedRound);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetRoundDto>>>> DeleteRound(int id)
        {
            var response = await _roundService.DeleteRound(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}