// Domain/Entities/Follow.cs
namespace Domain.Entities;

public sealed class Follow
{
    // Composite PK: FollowerId + FollowedId
    public long FollowerId { get; set; }
    public User? Follower { get; set; }

    public long FollowedId { get; set; }
    public User? Followed { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
