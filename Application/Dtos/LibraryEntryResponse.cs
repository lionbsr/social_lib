using Domain.Entities;
using Domain.Enums;

namespace Application.Dtos
{
    public class LibraryEntryResponse
    {
        public long UserId { get; set; }
        public long ContentId { get; set; }
        public LibraryStatus Status { get; set; }  // <-- ENUM

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

        public static LibraryEntryResponse FromEntity(LibraryEntry entry)
        {
            return new LibraryEntryResponse
            {
                UserId = entry.UserId,
                ContentId = entry.ContentId,
                Status = entry.Status,   // <-- ENUM → ENUM
                CreatedAt = entry.CreatedAt,
                UpdatedAt = entry.UpdatedAt
            };
        }
    }
}
