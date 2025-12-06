using Application.Dtos;

namespace Application.Interfaces
{
    public interface IActivityService
    {
        Task<List<ActivityResponse>> GetAllAsync();
        Task<List<ActivityResponse>> GetUserActivitiesAsync(long userId);
        Task<ActivityResponse> CreateAsync(ActivityCreateRequest request);
    }
}
