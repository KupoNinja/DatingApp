using System;
using System.Threading.Tasks;
using DatingApp.API.Interfaces;
using DatingApp.API.Models;
using DatingApp.API.Repository;

namespace DatingApp.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly AuthRepository _repo;

        public AuthService(AuthRepository repo)
        {
            _repo = repo;
        }

        public async Task<User> Register(User user, string password)
        {
            // Validate request
            user.UserName.ToLower();
            if (await _repo.UserExists(user.UserName))
                throw new Exception("User already exists");

            // var userToCreate = new User
            // {
            //     UserName = username
            // };

            var createdUser = await _repo.Register(user, password);

            return createdUser;
        }

        public Task<User> Login(string username, string password)
        {
            return null;
        }
    }
}