using Domain.Entities;

namespace Application.Dtos
{
    public class ListItemResponse
    {
        public long ContentId { get; set; }
        public string Title { get; set; } = "";
        public string? PosterUrl { get; set; }
        public short OrderIndex { get; set; }

        public static ListItemResponse FromEntity(ListItem item)
        {
            return new ListItemResponse
            {
                ContentId = item.ContentId,
                Title = item.Content?.Title ?? "",
                PosterUrl = item.Content?.PosterUrl,
                OrderIndex = item.OrderIndex
            };
        }
    }
}
