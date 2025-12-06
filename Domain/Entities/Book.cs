namespace Domain.Entities;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; }
    public string Authors { get; set; }
    public string? Description { get; set; }

    public int PageCount { get; set; }

    public string ReleaseYear { get; set; }   // 🔥 Eksik olan property
    public string Genres { get; set; }        // 🔥 Eksik olan property

    public string CoverUrl { get; set; }

    // Eğer başka navigation property varsa onları da ekleyebiliriz.
}
