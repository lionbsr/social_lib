// Domain/Entities/LibraryEntry.cs
using Domain.Enums;

namespace Domain.Entities;

public sealed class LibraryEntry
{
    // Composite PK: UserId + ContentId
    public long UserId { get; set; }
    public User? User { get; set; }

    public long ContentId { get; set; }
    public Content? Content { get; set; }

    public LibraryStatus Status { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
