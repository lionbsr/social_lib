using Application.Dtos;

namespace Application.Interfaces
{
    public interface ILibraryService
    {
        Task<List<LibraryEntryResponse>> GetUserLibraryAsync(long userId);
        Task<LibraryEntryResponse?> GetEntryAsync(long userId, long contentId);
        Task<LibraryEntryResponse> AddAsync(LibraryEntryRequest request);
        Task<LibraryEntryResponse> UpdateAsync(LibraryEntryUpdateRequest request);
        Task<bool> DeleteAsync(long userId, long contentId);
    }
}
