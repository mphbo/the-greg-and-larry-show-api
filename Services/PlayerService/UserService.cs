using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using the_greg_and_larry_show_api.Data;
using the_greg_and_larry_show_api.Dtos.User;
using the_greg_and_larry_show_api.Models;

namespace the_greg_and_larry_show_api.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
        {
            ServiceResponse<List<GetUserDto>> response = new ServiceResponse<List<GetUserDto>>();
            try
            {
                var dbUser = await _context.Users.FirstAsync(p => p.Id == id);
                _context.Users.Remove(dbUser);
                await _context.SaveChangesAsync();
                response.Data = _context.Users.Select(p => _mapper.Map<GetUserDto>(p)).ToList();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;

        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            var response = new ServiceResponse<List<GetUserDto>>();
            var dbUsers = await _context.Users.Include(p => p.Rounds).ToListAsync();
            response.Data = dbUsers.Select(p => _mapper.Map<GetUserDto>(p)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetUserDto>> GetUserById(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();
            var dbUser = await _context.Users.Include(p => p.Rounds).FirstOrDefaultAsync(p => p.Id == id);
            serviceResponse.Data = _mapper.Map<GetUserDto>(dbUser);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser)
        {
            ServiceResponse<GetUserDto> response = new ServiceResponse<GetUserDto>();
            try
            {
                var dbUser = await _context.Users.FirstOrDefaultAsync(p => p.Id == updatedUser.Id);

                if (dbUser != null)
                {
                    dbUser.FirstName = updatedUser.FirstName;
                    dbUser.LastName = updatedUser.LastName;
                }

                await _context.SaveChangesAsync();


                response.Data = _mapper.Map<GetUserDto>(dbUser);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}