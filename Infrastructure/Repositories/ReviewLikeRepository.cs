using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReviewLikeRepository : IReviewLikeRepository
    {
        private readonly AppDbContext _context;

        public ReviewLikeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsLikedAsync(long userId, long reviewId)
        {
            return await _context.ReviewLikes
                .AnyAsync(x => x.UserId == userId && x.ReviewId == reviewId);
        }

        public async Task AddLikeAsync(ReviewLike like)
        {
            _context.ReviewLikes.Add(like);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveLikeAsync(long userId, long reviewId)
        {
            var entity = await _context.ReviewLikes
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ReviewId == reviewId);

            if (entity != null)
            {
                _context.ReviewLikes.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetLikeCountAsync(long reviewId)
        {
            return await _context.ReviewLikes
                .CountAsync(x => x.ReviewId == reviewId);
        }

        public async Task<List<ReviewLike>> GetUserLikesAsync(long userId)
        {
            return await _context.ReviewLikes
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}
