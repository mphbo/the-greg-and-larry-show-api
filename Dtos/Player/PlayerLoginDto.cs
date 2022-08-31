using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace the_greg_and_larry_show_api.Dtos.Player
{
    public class PlayerLoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}