using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class LibraryEntryRepository : ILibraryEntryRepository
    {
        private readonly AppDbContext _context;

        public LibraryEntryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LibraryEntry?> GetAsync(long userId, long contentId)
        {
            return await _context.LibraryEntries
                .FirstOrDefaultAsync(x => x.UserId == userId && x.ContentId == contentId);
        }

        public async Task<List<LibraryEntry>> GetByUserAsync(long userId)
        {
            return await _context.LibraryEntries
                .Where(x => x.UserId == userId)
                .ToListAsync();
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
