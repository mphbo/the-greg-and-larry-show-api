using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace the_greg_and_larry_show_api.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(Player player, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> PlayerEmailExists(string email);
        Task<bool> PlayerUsernameExists(string username);
    }
}