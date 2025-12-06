
using Infrastructure.Persistence;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // 1) Kullanıcıyı ID ile getir
        public async Task<User?> GetByIdAsync(long id)
        {
            return await _context.Users
                .Include(u => u.Followers)
                .Include(u => u.Following)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        // 2) Kullanıcıyı Username ile getir
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Username == username);
        }

        // 3) Kullanıcıyı Email ile getir
        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        // 4) Kullanıcı arama (username veya fullName)
        public async Task<IEnumerable<User>> SearchAsync(string query)
        {
            query = query.ToLower();

            return await _context.Users
                .Where(u =>
                    u.Username.ToLower().Contains(query) ||
                    (u.FullName != null && u.FullName.ToLower().Contains(query))
                )
                .ToListAsync();
        }

        // 5) Kullanıcı ekleme
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        // 6) Kullanıcı güncelleme
        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // 7) Takip et
        public async Task FollowAsync(long followerId, long targetId)
        {
            if (followerId == targetId)
                throw new Exception("Kendi kendini takip edemezsin.");

            var exists = await _context.Follows
                .AnyAsync(x => x.FollowerId == followerId && x.FollowedId == targetId);

            if (!exists)
            {
                var follow = new Follow
                {
                    FollowerId = followerId,
                    FollowedId = targetId,
                    CreatedAt = DateTimeOffset.UtcNow
                };

                await _context.Follows.AddAsync(follow);
                await _context.SaveChangesAsync();
            }
        }

        // 8) Takibi bırak
        public async Task UnfollowAsync(long followerId, long targetId)
        {
            var follow = await _context.Follows
                .FirstOrDefaultAsync(x => x.FollowerId == followerId && x.FollowedId == targetId);

            if (follow != null)
            {
                _context.Follows.Remove(follow);
                await _context.SaveChangesAsync();
            }
        }

        // 9) Takip ediyor mu
        public async Task<bool> IsFollowingAsync(long followerId, long targetId)
        {
            return await _context.Follows
                .AnyAsync(x => x.FollowerId == followerId && x.FollowedId == targetId);
        }

        // 10) Takipçi listesi
        public async Task<IEnumerable<User>> GetFollowersAsync(long userId)
        {
            return await _context.Follows
                .Where(f => f.FollowedId == userId)
                .Select(f => f.Follower!)
                .ToListAsync();
        }

        // 11) Kullanıcının takip ettikleri
        public async Task<IEnumerable<User>> GetFollowingAsync(long userId)
        {
            return await _context.Follows
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followed!)
                .ToListAsync();
        }
    }
}
