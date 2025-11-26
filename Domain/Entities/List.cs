// Domain/Entities/List.cs
namespace Domain.Entities;

public sealed class List
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public User? User { get; set; }

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    public ICollection<ListItem> Items { get; set; } = new List<ListItem>();
}
