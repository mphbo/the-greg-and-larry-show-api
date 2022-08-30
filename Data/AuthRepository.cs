using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace the_greg_and_larry_show_api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public Task<ServiceResponse<string>> Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<int>> Register(Player player, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if (await PlayerEmailExists(player.Email))
            {
                response.Success = false;
                response.Message = "Player email already exists.";
                return response;
            }

            if (await PlayerUsernameExists(player.Username))
            {
                response.Success = false;
                response.Message = "Player username already exists.";
                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            player.PasswordHash = passwordHash;
            player.PasswordSalt = passwordSalt;

            _context.Players.Add(player);
            await _context.SaveChangesAsync();
            response.Data = player.Id;
            return response;
        }

        public async Task<bool> PlayerEmailExists(string email)
        {
            if (await _context.Players.AnyAsync(p => p.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> PlayerUsernameExists(string username)
        {
            if (await _context.Players.AnyAsync(p => p.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}