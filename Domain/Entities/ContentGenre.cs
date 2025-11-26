// Domain/Entities/ContentGenre.cs
namespace Domain.Entities;

public sealed class ContentGenre
{
    // Composite PK: ContentId + GenreId
    public long ContentId { get; set; }
    public Content? Content { get; set; }

    public short GenreId { get; set; }
    public Genre? Genre { get; set; }
}
