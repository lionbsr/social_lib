using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _users;

        public UserService(IUserRepository users)
        {
            _users = users;
        }

        // 1) Kullanıcıyı ID ile getir
        public async Task<User?> GetByIdAsync(long id)
        {
            return await _users.GetByIdAsync(id);
        }

        // 2) Arama
        public async Task<IEnumerable<User>> SearchAsync(string query)
        {
            return await _users.SearchAsync(query);
        }

        // 3) Kullanıcı güncelleme
        public async Task<User?> UpdateProfileAsync(long userId, string? fullName, string? bio, string? avatarUrl)
        {
            var user = await _users.GetByIdAsync(userId);

            if (user == null)
                return null;

            if (fullName != null) user.FullName = fullName;
            if (bio != null) user.Bio = bio;
            if (avatarUrl != null) user.AvatarUrl = avatarUrl;

            user.UpdatedAt = DateTime.UtcNow;

            await _users.UpdateAsync(user);

            return user;
        }

        // 4) Takip etme
        public async Task FollowAsync(long followerId, long targetId)
        {
            await _users.FollowAsync(followerId, targetId);
        }

        // 5) Takibi bırakma
        public async Task UnfollowAsync(long followerId, long targetId)
        {
            await _users.UnfollowAsync(followerId, targetId);
        }

        // 6) Takip ediyor mu?
        public async Task<bool> IsFollowingAsync(long followerId, long targetId)
        {
            return await _users.IsFollowingAsync(followerId, targetId);
        }

        // 7) Takipçi listesi
        public async Task<IEnumerable<User>> GetFollowersAsync(long userId)
        {
            return await _users.GetFollowersAsync(userId);
        }

        // 8) Kullanıcının takip ettikleri
        public async Task<IEnumerable<User>> GetFollowingAsync(long userId)
        {
            return await _users.GetFollowingAsync(userId);
        }
    }
}
