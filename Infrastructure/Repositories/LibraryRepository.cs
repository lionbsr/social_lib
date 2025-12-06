using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly AppDbContext _context;

        public LibraryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LibraryEntry>> GetUserLibraryAsync(long userId)
        {
            return await _context.LibraryEntries
                .Include(x => x.Content)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.UpdatedAt)
                .ToListAsync();
        }

        public async Task<LibraryEntry?> GetEntryAsync(long userId, long contentId)
        {
            return await _context.LibraryEntries
                .Include(x => x.Content)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ContentId == contentId);
        }

        public async Task AddAsync(LibraryEntry entry)
        {
            _context.LibraryEntries.Add(entry);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LibraryEntry entry)
        {
            _context.LibraryEntries.Update(entry);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(LibraryEntry entry)
        {
            _context.LibraryEntries.Remove(entry);
            await _context.SaveChangesAsync();
        }
    }
}
