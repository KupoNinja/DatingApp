using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Dtos;
using DatingApp.API.Interfaces;
using DatingApp.API.Models;
using DatingApp.API.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IAuthRepository _repo;

        public AuthService(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        public async Task<User> Register(UserForRegisterDto userForRegisterDto)
        {
            // Validate request
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            if (await _repo.UserExists(userForRegisterDto.Username))
                throw new Exception("User already exists");

            var userToCreate = new User
            {
                UserId = Guid.NewGuid().ToString(),
                Username = userForRegisterDto.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            return createdUser;
        }

        // See Build an App with ASPNET Core and Angular from Scratch, Section 3: Vid 35
        public async Task<object> Login(UserForLoginDto userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
            if (userFromRepo == null)
                return null;

            // Building the token
            // Token builds 2 claims
            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.UserId),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            // Ensuring key is valid
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            // Signing the key and encrypting
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Actual creation of token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            // Storing the token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenToReturn = new
            {
                token = tokenHandler.WriteToken(token)
            };

            return tokenToReturn;
        }
    }
}