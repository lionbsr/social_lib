using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFollowService
    {
        Task<(bool Success, string Message)> FollowUserAsync(long followerId, long followedId);
        Task<(bool Success, string Message)> UnfollowUserAsync(long followerId, long followedId);
        Task<List<User>> GetFollowersAsync(long userId);
        Task<List<User>> GetFollowingAsync(long userId);
    }
}
