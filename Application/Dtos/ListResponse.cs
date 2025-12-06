using Domain.Entities;

namespace Application.Dtos
{
    public class ListResponse
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        public List<ListItemResponse> Items { get; set; } = new();

        public static ListResponse FromEntity(List list)
        {
            return new ListResponse
            {
                Id = list.Id,
                UserId = list.UserId,
                Name = list.Name,
                Description = list.Description,
                Items = list.Items.Select(ListItemResponse.FromEntity).ToList()
            };
        }
    }
}
