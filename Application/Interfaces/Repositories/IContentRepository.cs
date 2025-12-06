using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IContentRepository
    {
        Task<Content?> GetByIdAsync(long id);
        Task<List<Content>> GetAllAsync();
        Task<List<Content>> GetByTypeAsync(string type);
        Task<List<Content>> SearchAsync(string query);
        Task AddAsync(Content content);
        Task UpdateAsync(Content content);
        Task DeleteAsync(Content content);
    }
}
