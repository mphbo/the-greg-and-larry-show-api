using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using the_greg_and_larry_show_api.Dtos.Player;
using the_greg_and_larry_show_api.Services;

namespace the_greg_and_larry_show_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetPlayerDto>>>> Get()
        {
            return Ok(await _playerService.GetAllPlayers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetPlayerDto>>> GetSingle(int id)
        {
            return Ok(await _playerService.GetPlayerById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetPlayerDto>>>> AddPlayer(AddPlayerDto newPlayer)
        {
            return Ok(await _playerService.AddPlayer(newPlayer));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetPlayerDto>>> UpdatePlayer(UpdatePlayerDto updatedPlayer)
        {
            var response = await _playerService.UpdatePlayer(updatedPlayer);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetPlayerDto>>>> DeletePlayer(int id)
        {
            var response = await _playerService.DeletePlayer(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}