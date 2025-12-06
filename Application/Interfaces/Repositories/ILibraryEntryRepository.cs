using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ILibraryEntryRepository
    {
        Task<LibraryEntry?> GetAsync(long userId, long contentId);
        Task<List<LibraryEntry>> GetByUserAsync(long userId);
        Task AddAsync(LibraryEntry entry);
        Task UpdateAsync(LibraryEntry entry);
        Task DeleteAsync(LibraryEntry entry);
    }
}
