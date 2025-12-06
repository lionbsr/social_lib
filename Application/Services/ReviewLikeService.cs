using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReviewLikeService : IReviewLikeService
    {
        private readonly IReviewLikeRepository _reviewLikeRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewLikeService(IReviewLikeRepository likeRepo, IReviewRepository reviewRepo)
        {
            _reviewLikeRepository = likeRepo;
            _reviewRepository = reviewRepo;
        }

        public async Task<(bool Success, string Message)> LikeReviewAsync(long userId, long reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review == null)
                return (false, "Yorum bulunamadı.");

            if (await _reviewLikeRepository.IsLikedAsync(userId, reviewId))
                return (false, "Bu yorumu zaten beğendiniz.");

            var like = new ReviewLike
            {
                UserId = userId,
                ReviewId = reviewId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _reviewLikeRepository.AddLikeAsync(like);

            return (true, "Beğenildi.");
        }

        public async Task<(bool Success, string Message)> UnlikeReviewAsync(long userId, long reviewId)
        {
            if (!await _reviewLikeRepository.IsLikedAsync(userId, reviewId))
                return (false, "Bu yorumu zaten beğenmemişsiniz.");

            await _reviewLikeRepository.RemoveLikeAsync(userId, reviewId);
            return (true, "Beğeni kaldırıldı.");
        }

        public Task<int> GetLikeCountAsync(long reviewId)
        {
            return _reviewLikeRepository.GetLikeCountAsync(reviewId);
        }

        public Task<bool> IsLikedAsync(long userId, long reviewId)
        {
            return _reviewLikeRepository.IsLikedAsync(userId, reviewId);
        }
    }
}
