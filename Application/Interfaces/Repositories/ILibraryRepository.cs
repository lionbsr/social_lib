using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ILibraryRepository
    {
        Task<List<LibraryEntry>> GetUserLibraryAsync(long userId);
        Task<LibraryEntry?> GetEntryAsync(long userId, long contentId);
        Task AddAsync(LibraryEntry entry);
        Task UpdateAsync(LibraryEntry entry);
        Task DeleteAsync(LibraryEntry entry);
    }
}
