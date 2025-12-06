using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IFollowRepository
    {
        Task<bool> FollowExistsAsync(long followerId, long followedId);
        Task AddFollowAsync(Follow follow);
        Task RemoveFollowAsync(long followerId, long followedId);
        Task<List<User>> GetFollowersAsync(long userId);
        Task<List<User>> GetFollowingAsync(long userId);
    }
}
