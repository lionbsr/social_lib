// Domain/Entities/Rating.cs
namespace Domain.Entities;

public sealed class Rating
{
    // Composite PK: UserId + ContentId
    public long UserId { get; set; }
    public User? User { get; set; }

    public long ContentId { get; set; }
    public Content? Content { get; set; }

    public short Score { get; set; } // 1..10
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}
