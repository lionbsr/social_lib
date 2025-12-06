namespace Application.DTOs;

public class TmdbSearchResult
{
    public List<TmdbMovieBasic> results { get; set; }
}

public class TmdbMovieBasic
{
    public int id { get; set; }
    public string title { get; set; }
    public string overview { get; set; }
    public string release_date { get; set; }
    public string poster_path { get; set; }
}

public class TmdbMovieDetail
{
    public string title { get; set; }
    public string overview { get; set; }
    public string release_date { get; set; }
    public string poster_path { get; set; }

    public List<TmdbGenre> genres { get; set; }
    public TmdbCredits credits { get; set; }
}

public class TmdbGenre
{
    public string name { get; set; }
}

public class TmdbCredits
{
    public List<TmdbCast> cast { get; set; }
    public List<TmdbCrew> crew { get; set; }
}

public class TmdbCast
{
    public string name { get; set; }
}

public class TmdbCrew
{
    public string job { get; set; }
    public string name { get; set; }
}
