using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ListRepository : IListRepository
    {
        private readonly AppDbContext _context;

        public ListRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.List>> GetUserListsAsync(long userId)
        {
            return await _context.Lists
                .Include(x => x.Items)
                .ThenInclude(i => i.Content)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<Domain.Entities.List?> GetByIdAsync(long id)
        {
            return await _context.Lists
                .Include(x => x.Items)
                .ThenInclude(i => i.Content)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(Domain.Entities.List list)
        {
            _context.Lists.Add(list);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Domain.Entities.List list)
        {
            _context.Lists.Update(list);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Domain.Entities.List list)
        {
            _context.Lists.Remove(list);
            await _context.SaveChangesAsync();
        }

        public async Task AddItemAsync(ListItem item)
        {
            _context.ListItems.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemAsync(ListItem item)
        {
            _context.ListItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<ListItem?> GetItemAsync(long listId, long contentId)
        {
            return await _context.ListItems
                .Include(x => x.Content)
                .FirstOrDefaultAsync(x => x.ListId == listId && x.ContentId == contentId);
        }
    }
}
