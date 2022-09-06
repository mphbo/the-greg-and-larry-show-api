using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using the_greg_and_larry_show_api.Dtos.User;

namespace the_greg_and_larry_show_api.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<GetUserDto>> GetUserById(int id);
        Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto User);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id);
    }
}