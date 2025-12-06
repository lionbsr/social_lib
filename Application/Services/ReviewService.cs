using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepo;

        public ReviewService(IReviewRepository reviewRepo)
        {
            _reviewRepo = reviewRepo;
        }

        public async Task<(bool Success, string Message)> AddReviewAsync(long userId, long contentId, string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return (false, "Yorum metni boş olamaz.");

            var review = new Review
            {
                UserId = userId,
                ContentId = contentId,
                Body = text,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _reviewRepo.AddAsync(review);
            return (true, "Yorum başarıyla eklendi.");
        }

        public async Task<(bool Success, string Message)> DeleteReviewAsync(long userId, long reviewId)
        {
            var review = await _reviewRepo.GetByIdAsync(reviewId);

            if (review == null)
                return (false, "Yorum bulunamadı.");

            if (review.UserId != userId)
                return (false, "Bu yorumu silme yetkiniz yok.");

            await _reviewRepo.DeleteAsync(review);
            return (true, "Yorum silindi.");
        }

        public async Task<List<Review>> GetReviewsForContentAsync(long contentId)
        {
            return await _reviewRepo.GetReviewsByContentAsync(contentId);
        }

        public async Task<List<Review>> GetReviewsForUserAsync(long userId)
        {
            return await _reviewRepo.GetReviewsByUserAsync(userId);
        }
    }
}
