using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IContentService
    {
        Task<Content?> GetByIdAsync(long id);
        Task<List<Content>> GetAllAsync();
        Task<List<Content>> GetByTypeAsync(string type);
        Task<List<Content>> SearchAsync(string query);
        Task<(bool Success, string Message)> AddAsync(Content content);
        Task<(bool Success, string Message)> UpdateAsync(Content content);
        Task<(bool Success, string Message)> DeleteAsync(long id);
    }
}
