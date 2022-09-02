using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using the_greg_and_larry_show_api.Dtos.Round;

namespace the_greg_and_larry_show_api.Dtos.User
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; }
        public List<GetRoundDto> Rounds { get; set; }
    }
}