using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly AppDbContext _context;

        public FollowRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> FollowExistsAsync(long followerId, long followedId)
        {
            return await _context.Follows
                .AnyAsync(x => x.FollowerId == followerId && x.FollowedId == followedId);
        }

        public async Task AddFollowAsync(Follow follow)
        {
            _context.Follows.Add(follow);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveFollowAsync(long followerId, long followedId)
        {
            var follow = await _context.Follows
                .FirstOrDefaultAsync(x => x.FollowerId == followerId && x.FollowedId == followedId);

            if (follow != null)
            {
                _context.Follows.Remove(follow);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<User>> GetFollowersAsync(long userId)
        {
            return await _context.Follows
                .Where(x => x.FollowedId == userId)
                .Select(x => x.Follower!)
                .ToListAsync();
        }

        public async Task<List<User>> GetFollowingAsync(long userId)
        {
            return await _context.Follows
                .Where(x => x.FollowerId == userId)
                .Select(x => x.Followed!)
                .ToListAsync();
        }
    }
}
