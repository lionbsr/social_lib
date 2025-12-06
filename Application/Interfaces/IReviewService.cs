using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReviewService
    {
        Task<(bool Success, string Message)> AddReviewAsync(long userId, long contentId, string text);
        Task<(bool Success, string Message)> DeleteReviewAsync(long userId, long reviewId);
        Task<List<Review>> GetReviewsForContentAsync(long contentId);
        Task<List<Review>> GetReviewsForUserAsync(long userId);
    }
}
