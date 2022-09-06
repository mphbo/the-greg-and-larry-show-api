using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using the_greg_and_larry_show_api.Dtos.User;

namespace the_greg_and_larry_show_api.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthRepository(DataContext context, IConfiguration configuration, IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<LoginResponse>> Login(string email, string password)
        {
            var response = new ServiceResponse<LoginResponse>();
            var User = await _context.Users.Include(u => u.Rounds).FirstOrDefaultAsync(p => p.Email.ToLower().Equals(email.ToLower()));

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
                response.Data = new LoginResponse
                {
                    Token = CreateToken(User),
                    User = _mapper.Map<GetUserDto>(User)
                };
            }
            return response;
        }

        public async Task<ServiceResponse<LoginResponse>> Register(User user, string password)
        {
            ServiceResponse<LoginResponse> response = new ServiceResponse<LoginResponse>();
            if (await UserEmailExists(user.Email))
            {
                response.Success = false;
                response.Message = "User email already exists.";
                return response;
            }

            if (await UserUsernameExists(user.Username))
            {
                response.Success = false;
                response.Message = "User username already exists.";
                return response;
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return await this.Login(user.Email, password);
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