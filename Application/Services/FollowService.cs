using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepo;

        public FollowService(IFollowRepository followRepo)
        {
            _followRepo = followRepo;
        }

        public async Task<(bool Success, string Message)> FollowUserAsync(long followerId, long followedId)
        {
            if (followerId == followedId)
                return (false, "Kendinizi takip edemezsiniz.");

            if (await _followRepo.FollowExistsAsync(followerId, followedId))
                return (false, "Bu kullanıcı zaten takip ediliyor.");

            var follow = new Follow
            {
                FollowerId = followerId,
                FollowedId = followedId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _followRepo.AddFollowAsync(follow);

            return (true, "Takip edildi.");
        }

        public async Task<(bool Success, string Message)> UnfollowUserAsync(long followerId, long followedId)
        {
            if (!await _followRepo.FollowExistsAsync(followerId, followedId))
                return (false, "Bu kullanıcı zaten takip edilmiyor.");

            await _followRepo.RemoveFollowAsync(followerId, followedId);

            return (true, "Takipten çıkıldı.");
        }

        public async Task<List<User>> GetFollowersAsync(long userId)
        {
            return await _followRepo.GetFollowersAsync(userId);
        }

        public async Task<List<User>> GetFollowingAsync(long userId)
        {
            return await _followRepo.GetFollowingAsync(userId);
        }
    }
}
