using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IListRepository
    {
        Task<List<Domain.Entities.List>> GetUserListsAsync(long userId);
        Task<Domain.Entities.List?> GetByIdAsync(long id);
        Task AddAsync(Domain.Entities.List list);
        Task UpdateAsync(Domain.Entities.List list);
        Task DeleteAsync(Domain.Entities.List list);

        // Items
        Task AddItemAsync(ListItem item);
        Task RemoveItemAsync(ListItem item);
        Task<ListItem?> GetItemAsync(long listId, long contentId);
    }
}
