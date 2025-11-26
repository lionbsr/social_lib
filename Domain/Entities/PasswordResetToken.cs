// Domain/Entities/PasswordResetToken.cs
namespace Domain.Entities;

public sealed class PasswordResetToken
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User? User { get; set; }

    public string TokenHash { get; set; } = null!;
    public DateTimeOffset ExpiresAt { get; set; }
    public bool Used { get; set; } = false;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
