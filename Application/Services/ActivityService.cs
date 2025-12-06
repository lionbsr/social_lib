using Application.Dtos;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _repo;

        public ActivityService(IActivityRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ActivityResponse>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(ActivityResponse.FromEntity).ToList();
        }

        public async Task<List<ActivityResponse>> GetUserActivitiesAsync(long userId)
        {
            var list = await _repo.GetUserActivitiesAsync(userId);
            return list.Select(ActivityResponse.FromEntity).ToList();
        }

        public async Task<ActivityResponse> CreateAsync(ActivityCreateRequest request)
        {
            var a = new Activity
            {
                UserId = request.UserId,
                ContentId = request.ContentId,
                ReviewId = request.ReviewId,
                ListId = request.ListId,
                PayloadJson = request.PayloadJson,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _repo.AddAsync(a);
            return ActivityResponse.FromEntity(a);
        }
    }
}
