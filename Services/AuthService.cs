using System;
using System.Threading.Tasks;
using DatingApp.API.Dtos;
using DatingApp.API.Interfaces;
using DatingApp.API.Models;
using DatingApp.API.Repository;

namespace DatingApp.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repo;

        public AuthService(IAuthRepository repo)
        {
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

        public Task<User> Login(string username, string password)
        {
            return null;
        }
    }
}