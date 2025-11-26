// Domain/Entities/Activity.cs
using Domain.Enums;

namespace Domain.Entities;

public sealed class Activity
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User? User { get; set; }

    public ActivityType Type { get; set; }

    // nullable foreign keys depending on activity type
    public long? ContentId { get; set; }
    public Content? Content { get; set; }

    public long? ReviewId { get; set; }
    public Review? Review { get; set; }

    public long? ListId { get; set; }
    public List? List { get; set; }

    public string? PayloadJson { get; set; } // small json payload for extra metadata
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
