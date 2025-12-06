using Application.Dtos;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository _repo;

        public LibraryService(ILibraryRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<LibraryEntryResponse>> GetUserLibraryAsync(long userId)
        {
            var list = await _repo.GetUserLibraryAsync(userId);
            return list.Select(LibraryEntryResponse.FromEntity).ToList();
        }

        public async Task<LibraryEntryResponse?> GetEntryAsync(long userId, long contentId)
        {
            var entry = await _repo.GetEntryAsync(userId, contentId);
            return entry == null ? null : LibraryEntryResponse.FromEntity(entry);
        }

        public async Task<LibraryEntryResponse> AddAsync(LibraryEntryRequest request)
        {
            var entry = new LibraryEntry
            {
                UserId = request.UserId,
                ContentId = request.ContentId,
                Status = request.Status,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            await _repo.AddAsync(entry);
            return LibraryEntryResponse.FromEntity(entry);
        }

        public async Task<LibraryEntryResponse> UpdateAsync(LibraryEntryUpdateRequest request)
        {
            var existing = await _repo.GetEntryAsync(request.UserId, request.ContentId);
            if (existing == null)
                throw new Exception("Entry not found");

            existing.Status = request.Status;
            existing.UpdatedAt = DateTimeOffset.UtcNow;

            await _repo.UpdateAsync(existing);
            return LibraryEntryResponse.FromEntity(existing);
        }

        public async Task<bool> DeleteAsync(long userId, long contentId)
        {
            var entry = await _repo.GetEntryAsync(userId, contentId);
            if (entry == null)
                return false;

            await _repo.DeleteAsync(entry);
            return true;
        }
    }
}
