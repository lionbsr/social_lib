using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IRatingRepository
    {
        Task<Rating> GetRatingAsync(long userId, long contentId);
        Task AddAsync(Rating rating);
        Task UpdateAsync(Rating rating);
        Task DeleteAsync(Rating rating);
        Task<double> GetAverageRatingAsync(long contentId);
        Task<int> GetRatingCountAsync(long contentId);
    }
}
