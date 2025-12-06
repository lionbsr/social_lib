using Domain.Entities;

namespace Application.Dtos
{
    public class ActivityResponse
    {
        public long Id { get; set; }
        public long UserId { get; set; }

        public long? ContentId { get; set; }
        public string? ContentTitle { get; set; }

        public long? ReviewId { get; set; }
        public long? ListId { get; set; }
        public string PayloadJson { get; set; } = "{}";
        public DateTimeOffset CreatedAt { get; set; }

        public static ActivityResponse FromEntity(Activity a)
        {
            return new ActivityResponse
            {
                Id = a.Id,
                UserId = a.UserId,
                ContentId = a.ContentId,
                ContentTitle = a.Content?.Title,
                ReviewId = a.ReviewId,
                ListId = a.ListId,
                PayloadJson = a.PayloadJson ?? "{}",
                CreatedAt = a.CreatedAt
            };
        }
    }
}
