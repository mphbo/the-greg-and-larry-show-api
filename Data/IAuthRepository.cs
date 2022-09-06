using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace the_greg_and_larry_show_api.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<LoginResponse>> Register(User User, string password);
        Task<ServiceResponse<LoginResponse>> Login(string username, string password);
        Task<bool> UserEmailExists(string email);
        Task<bool> UserUsernameExists(string username);
    }
}