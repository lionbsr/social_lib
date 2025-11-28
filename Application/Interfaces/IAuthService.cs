using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(string username, string email, string password);
        Task<string> LoginAsync(string usernameOrEmail, string password);
        Task<string> RefreshTokenAsync(string refreshToken);
    }
}
