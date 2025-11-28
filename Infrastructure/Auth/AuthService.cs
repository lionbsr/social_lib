using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using BCrypt.Net;


namespace Infrastructure.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository users, AppDbContext context, IConfiguration config)
        {
            _users = users;
            _context = context;
            _config = config;
        }

        public async Task<User> RegisterAsync(string username, string email, string password)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            await _users.AddAsync(user);
            await _users.SaveChangesAsync();

            return user;
        }

        public async Task<string> LoginAsync(string usernameOrEmail, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Username == usernameOrEmail || x.Email == usernameOrEmail);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                throw new Exception("Invalid credentials");

            return GenerateJwtToken(user);
        }

        public Task<string> RefreshTokenAsync(string refreshToken)
        {
            // Şimdilik basit versiyon, sonra genişletiriz
            return Task.FromResult(refreshToken);
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("username", user.Username)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(6),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
