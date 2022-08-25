using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using the_greg_and_larry_show_api.Services;

namespace the_greg_and_larry_show_api.Controllers
{
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
        public ActionResult<List<Player>> Get()
        {
            return Ok(_playerService.GetAllPlayers());
        }

        [HttpGet("{id}")]
        public ActionResult<Player> GetSingle(int id)
        {
            return Ok(_playerService.GetPlayerById(id));
        }

        [HttpPost]
        public ActionResult<List<Player>> AddPlayer(Player newPlayer)
        {
            return Ok(_playerService.AddPlayer(newPlayer));
        }
    }
}