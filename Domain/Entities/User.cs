// Domain/Entities/User.cs
using System.Collections.Generic;
using System.Diagnostics;

namespace Domain.Entities;

public sealed class User
{
    public long Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string? Bio { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public string? FullName { get; set; }
    public string? ProfileImage { get; set; }


    // Social relations (follows)
    // - Users this user follows (following)
    public ICollection<Follow> Following { get; set; } = new List<Follow>();
    // - Users who follow this user (followers)
    public ICollection<Follow> Followers { get; set; } = new List<Follow>();

    // Auth tokens
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public ICollection<PasswordResetToken> PasswordResetTokens { get; set; } = new List<PasswordResetToken>();

    // Library, ratings, reviews
    public ICollection<LibraryEntry> LibraryEntries { get; set; } = new List<LibraryEntry>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<ReviewLike> ReviewLikes { get; set; } = new List<ReviewLike>();

    // Lists and activities
    public ICollection<List> Lists { get; set; } = new List<List>();
    public ICollection<Activity> Activities { get; set; } = new List<Activity>();
}
