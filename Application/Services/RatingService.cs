using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepo;
        private readonly IReviewRepository _reviewRepo;

        public RatingService(IRatingRepository ratingRepo, IReviewRepository reviewRepo)
        {
            _ratingRepo = ratingRepo;
            _reviewRepo = reviewRepo;
        }

        public async Task<(bool Success, string Message)> RateAsync(long userId, long contentId, int score)
        {
            if (score < 1 || score > 10)
                return (false, "Puan 1 ile 10 arasında olmalıdır.");

            var existing = await _ratingRepo.GetRatingAsync(userId, contentId);

            if (existing == null)
            {
                var rating = new Rating
                {
                    UserId = userId,
                    ContentId = contentId,
                    Score = (short)score
                };

                await _ratingRepo.AddAsync(rating);
                return (true, "Puan eklendi.");
            }
            else
            {
                existing.Score = (short)score;
                await _ratingRepo.UpdateAsync(existing);
                return (true, "Puan güncellendi.");
            }
        }

        public async Task<(bool Success, string Message)> RemoveRatingAsync(long userId, long contentId)
        {
            var existing = await _ratingRepo.GetRatingAsync(userId, contentId);

            if (existing == null)
                return (false, "Bu içeriğe daha önce puan vermemişsiniz.");

            await _ratingRepo.DeleteAsync(existing);
            return (true, "Puan kaldırıldı.");
        }

        public Task<double> GetAverageRatingAsync(long contentId)
        {
            return _ratingRepo.GetAverageRatingAsync(contentId);
        }

        public Task<int> GetRatingCountAsync(long contentId)
        {
            return _ratingRepo.GetRatingCountAsync(contentId);
        }

        public async Task<int?> GetUserRatingAsync(long userId, long contentId)
        {
            var rating = await _ratingRepo.GetRatingAsync(userId, contentId);
            return rating?.Score;
        }
    }
}
