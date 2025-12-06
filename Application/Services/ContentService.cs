using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ContentService : IContentService
    {
        private readonly IContentRepository _repo;

        public ContentService(IContentRepository repo)
        {
            _repo = repo;
        }

        public Task<Content?> GetByIdAsync(long id)
        {
            return _repo.GetByIdAsync(id);
        }

        public Task<List<Content>> GetAllAsync()
        {
            return _repo.GetAllAsync();
        }

        public Task<List<Content>> GetByTypeAsync(string type)
        {
            return _repo.GetByTypeAsync(type);
        }

        public Task<List<Content>> SearchAsync(string query)
        {
            return _repo.SearchAsync(query);
        }

        public async Task<(bool Success, string Message)> AddAsync(Content content)
        {
            await _repo.AddAsync(content);
            return (true, "İçerik başarıyla eklendi.");
        }

        public async Task<(bool Success, string Message)> UpdateAsync(Content content)
        {
            await _repo.UpdateAsync(content);
            return (true, "İçerik güncellendi.");
        }

        public async Task<(bool Success, string Message)> DeleteAsync(long id)
        {
            var c = await _repo.GetByIdAsync(id);
            if (c == null)
                return (false, "İçerik bulunamadı.");

            await _repo.DeleteAsync(c);
            return (true, "İçerik silindi.");
        }
    }
}
