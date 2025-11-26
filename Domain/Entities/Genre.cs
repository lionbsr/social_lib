// Domain/Entities/Genre.cs
namespace Domain.Entities;

public sealed class Genre
{
    public short Id { get; set; }
    public string Name { get; set; } = null!;
    public short Domain { get; set; } // 1=Movie,2=Book,3=Both

    public ICollection<ContentGenre> ContentGenres { get; set; } =  new List<ContentGenre>();
}
