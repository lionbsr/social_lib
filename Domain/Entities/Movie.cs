namespace Domain.Entities;

public class Movie
{
    public long Id { get; set; }

    public string Title { get; set; }
    public string Overview { get; set; }
    public string ReleaseYear { get; set; }

    public string Director { get; set; }
    public string Actors { get; set; }
    public string Genres { get; set; }

    public string PosterUrl { get; set; }
}
