// Domain/Entities/ListItem.cs
namespace Domain.Entities;

public sealed class ListItem
{
    // Composite PK: ListId + ContentId
    public long ListId { get; set; }
    public List? List { get; set; }

    public long ContentId { get; set; }
    public Content? Content { get; set; }

    public short OrderIndex { get; set; } = 0;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
