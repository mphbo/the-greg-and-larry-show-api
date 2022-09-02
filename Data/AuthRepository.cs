using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace the_greg_and_larry_show_api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
            var User = await _context.Users.FirstOrDefaultAsync(p => p.Email.ToLower().Equals(email.ToLower()));

            if (User == null)
            {
                response.Success = false;
                response.Message = "Email address not found.";
            }
            else if (!VerifyPasswordHash(password, User.PasswordHash, User.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Incorrect password.";
            }
            else
            {
                response.Data = CreateToken(User);
            }
            return response;
        }

        public async Task<ServiceResponse<int>> Register(User User, string password)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            if (await UserEmailExists(User.Email))
            {
                response.Success = false;
                response.Message = "User email already exists.";
                return response;
            }

            if (await UserUsernameExists(User.Username))
            {
                response.Success = false;
                response.Message = "User username already exists.";
                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            User.PasswordHash = passwordHash;
            User.PasswordSalt = passwordSalt;

            _context.Users.Add(User);
            await _context.SaveChangesAsync();
            response.Data = User.Id;
            return response;
        }

        public async Task<bool> UserEmailExists(string email)
        {
            if (await _context.Users.AnyAsync(p => p.Email.ToLower() == email.ToLower()))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UserUsernameExists(string username)
        {
            if (await _context.Users.AnyAsync(p => p.Username.ToLower() == username.ToLower()))
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

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);

            }
        }

        private string CreateToken(User User)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
                new Claim(ClaimTypes.Name, User.Email)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}