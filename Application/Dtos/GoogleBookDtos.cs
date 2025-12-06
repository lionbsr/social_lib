namespace Application.DTOs;

public class GoogleBooksResult
{
    public List<GoogleBookItem> items { get; set; }
}

public class GoogleBookItem
{
    public GoogleBookVolumeInfo volumeInfo { get; set; }
}

public class GoogleBookVolumeInfo
{
    public string title { get; set; }
    public List<string> authors { get; set; }
    public string description { get; set; }
    public int? pageCount { get; set; }
    public List<string> categories { get; set; }
    public string publishedDate { get; set; }
    public GoogleBookImageLinks imageLinks { get; set; }
}

public class GoogleBookImageLinks
{
    public string thumbnail { get; set; }
}
