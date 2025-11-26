// Domain/Entities/RefreshToken.cs
namespace Domain.Entities;

public sealed class RefreshToken
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User? User { get; set; }

    public string TokenHash { get; set; } = null!;
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public bool Revoked { get; set; } = false;
}
