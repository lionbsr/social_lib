// Domain/Entities/ReviewLike.cs
namespace Domain.Entities;

public sealed class ReviewLike
{
    // Composite PK: UserId + ReviewId
    public long UserId { get; set; }
    public User? User { get; set; }

    public long ReviewId { get; set; }
    public Review? Review { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
