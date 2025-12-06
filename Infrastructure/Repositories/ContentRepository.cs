using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly AppDbContext _context;

        public ContentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Content?> GetByIdAsync(long id)
        {
            return await _context.Contents
                .Include(c => c.ContentGenres)
                    .ThenInclude(g => g.Genre)
                .Include(c => c.Reviews)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Content>> GetAllAsync()
        {
            return await _context.Contents
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Content>> GetByTypeAsync(string type)
        {
            // string → enum dönüşümü
            if (!Enum.TryParse<ContentKind>(type, true, out var parsedType))
                return new List<Content>();

            return await _context.Contents
                .Where(c => c.Type == parsedType)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Content>> SearchAsync(string query)
        {
            var q = query.ToLower();

            return await _context.Contents
                .Where(c =>
                    c.Title.ToLower().Contains(q) ||
                    (c.Description != null && c.Description.ToLower().Contains(q))
                )
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task AddAsync(Content content)
        {
            _context.Contents.Add(content);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Content content)
        {
            _context.Contents.Update(content);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Content content)
        {
            _context.Contents.Remove(content);
            await _context.SaveChangesAsync();
        }
    }
}
