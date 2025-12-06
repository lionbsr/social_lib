using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRatingService
    {
        Task<(bool Success, string Message)> RateAsync(long userId, long contentId, int score);
        Task<(bool Success, string Message)> RemoveRatingAsync(long userId, long contentId);
        Task<double> GetAverageRatingAsync(long contentId);
        Task<int> GetRatingCountAsync(long contentId);
        Task<int?> GetUserRatingAsync(long userId, long contentId);
    }
}
