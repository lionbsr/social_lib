using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReviewLikeService
    {
        Task<(bool Success, string Message)> LikeReviewAsync(long userId, long reviewId);
        Task<(bool Success, string Message)> UnlikeReviewAsync(long userId, long reviewId);
        Task<int> GetLikeCountAsync(long reviewId);
        Task<bool> IsLikedAsync(long userId, long reviewId);
    }
}
