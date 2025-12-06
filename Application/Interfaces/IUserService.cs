using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(long id);
        Task<IEnumerable<User>> SearchAsync(string query);
        Task<User?> UpdateProfileAsync(long userId, string? fullName, string? bio, string? avatarUrl);

        Task FollowAsync(long followerId, long targetId);
        Task UnfollowAsync(long followerId, long targetId);
        Task<bool> IsFollowingAsync(long followerId, long targetId);

        Task<IEnumerable<User>> GetFollowersAsync(long userId);
        Task<IEnumerable<User>> GetFollowingAsync(long userId);
    }
}
