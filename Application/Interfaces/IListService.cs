using Application.Dtos;

namespace Application.Interfaces
{
    public interface IListService
    {
        Task<List<ListResponse>> GetUserListsAsync(long userId);
        Task<ListResponse?> GetByIdAsync(long id);
        Task<ListResponse> CreateAsync(ListCreateRequest request);
        Task<ListResponse> UpdateAsync(ListUpdateRequest request);
        Task<bool> DeleteAsync(long id);

        Task<ListResponse> AddItemAsync(ListItemAddRequest request);
        Task<bool> RemoveItemAsync(long listId, long contentId);
    }
}
