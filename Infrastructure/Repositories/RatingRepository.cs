using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly AppDbContext _context;

        public RatingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Rating> GetRatingAsync(long userId, long contentId)
        {
            return await _context.Ratings
                .FirstOrDefaultAsync(r => r.UserId == userId && r.ContentId == contentId);
        }

        public async Task AddAsync(Rating rating)
        {
            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Rating rating)
        {
            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
        }

        public async Task<double> GetAverageRatingAsync(long contentId)
        {
            var ratings = await _context.Ratings
                .Where(r => r.ContentId == contentId)
                .ToListAsync();

            if (!ratings.Any()) return 0;

            return ratings.Average(r => r.Score);
        }

        public async Task<int> GetRatingCountAsync(long contentId)
        {
            return await _context.Ratings
                .CountAsync(r => r.ContentId == contentId);
        }
    }
}
