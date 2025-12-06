using Application.Dtos;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public class ListService : IListService
    {
        private readonly IListRepository _repo;

        public ListService(IListRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ListResponse>> GetUserListsAsync(long userId)
        {
            var lists = await _repo.GetUserListsAsync(userId);
            return lists.Select(ListResponse.FromEntity).ToList();
        }

        public async Task<ListResponse?> GetByIdAsync(long id)
        {
            var list = await _repo.GetByIdAsync(id);
            return list == null ? null : ListResponse.FromEntity(list);
        }

        public async Task<ListResponse> CreateAsync(ListCreateRequest request)
        {
            var list = new Domain.Entities.List
            {
                UserId = request.UserId,
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow
            };

            await _repo.AddAsync(list);
            return ListResponse.FromEntity(list);
        }

        public async Task<ListResponse> UpdateAsync(ListUpdateRequest request)
        {
            var list = await _repo.GetByIdAsync(request.Id);
            if (list == null) throw new Exception("List not found");

            list.Name = request.Name;
            list.Description = request.Description;
            list.UpdatedAt = DateTimeOffset.UtcNow;

            await _repo.UpdateAsync(list);
            return ListResponse.FromEntity(list);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var list = await _repo.GetByIdAsync(id);
            if (list == null) return false;

            await _repo.DeleteAsync(list);
            return true;
        }

        public async Task<ListResponse> AddItemAsync(ListItemAddRequest request)
        {
            var item = new ListItem
            {
                ListId = request.ListId,
                ContentId = request.ContentId,
                OrderIndex = request.OrderIndex,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _repo.AddItemAsync(item);

            var list = await _repo.GetByIdAsync(request.ListId);
            return ListResponse.FromEntity(list!);
        }

        public async Task<bool> RemoveItemAsync(long listId, long contentId)
        {
            var existing = await _repo.GetItemAsync(listId, contentId);
            if (existing == null) return false;

            await _repo.RemoveItemAsync(existing);
            return true;
        }
    }
}
