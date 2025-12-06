using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IReviewLikeRepository
    {
        Task<bool> IsLikedAsync(long userId, long reviewId);
        Task AddLikeAsync(ReviewLike like);
        Task RemoveLikeAsync(long userId, long reviewId);
        Task<int> GetLikeCountAsync(long reviewId);
        Task<List<ReviewLike>> GetUserLikesAsync(long userId);
    }
}
