using Domain.Enums;

namespace Domain.Entities
{
    public class LibraryEntry
    {
        public long UserId { get; set; }
        public User? User { get; set; }

        public long ContentId { get; set; }
        public Content? Content { get; set; }

        public LibraryStatus Status { get; set; }  // <-- ENUM !
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}
