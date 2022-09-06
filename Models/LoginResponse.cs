using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using the_greg_and_larry_show_api.Dtos.User;

namespace the_greg_and_larry_show_api.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public GetUserDto User { get; set; }
    }
}