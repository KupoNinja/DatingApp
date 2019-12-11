using DatingApp.API.Interfaces;

namespace DatingApp.API.Models
{
    public class User : IUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}