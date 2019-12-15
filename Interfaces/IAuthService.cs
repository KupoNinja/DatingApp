using System.Threading.Tasks;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Interfaces
{
    public interface IAuthService
    {
        Task<User> Register(UserForRegisterDto userForRegisterDto);
        Task<object> Login(UserForLoginDto userForLoginDto);
    }
}