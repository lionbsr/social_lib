using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IActivityRepository
    {
        Task<List<Activity>> GetAllAsync();
        Task<List<Activity>> GetUserActivitiesAsync(long userId);
        Task AddAsync(Activity activity);
    }
}
