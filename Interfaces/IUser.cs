namespace DatingApp.API.Interfaces
{
    public interface IUser
    {
        string UserId { get; set; }
        string Username { get; set; }
        byte[] PasswordHash { get; set; }
        byte[] PasswordSalt { get; set; }
    }
}