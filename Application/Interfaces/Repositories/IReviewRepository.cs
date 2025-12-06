using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IReviewRepository
    {
        Task<Review> GetByIdAsync(long id);
        Task<List<Review>> GetReviewsByContentAsync(long contentId);
        Task<List<Review>> GetReviewsByUserAsync(long userId);
        Task AddAsync(Review review);
        Task DeleteAsync(Review review);
        Task SaveAsync();
    }
}
