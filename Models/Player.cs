using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace the_greg_and_larry_show_api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Round>? Rounds { get; set; }
    }
}