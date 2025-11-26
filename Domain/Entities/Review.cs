// Domain/Entities/Review.cs
namespace Domain.Entities;

public sealed class Review
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User? User { get; set; }

    public long ContentId { get; set; }
    public Content? Content { get; set; }

    public string Body { get; set; } = null!;
    public short? Score { get; set; } // optional (1..10)
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public ICollection<ReviewLike> Likes { get; set; } = new List<ReviewLike>();
}
