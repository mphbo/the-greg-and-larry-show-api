using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace the_greg_and_larry_show_api.Dtos.Player
{
    public class AddPlayerDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Password { get; set; }
    }
}