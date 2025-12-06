using System.Net.Http;
using System.Text.Json;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Services;

public class BookImportService : IBookImportService
{
    private readonly HttpClient _http;
    private readonly AppDbContext _db;

    public BookImportService(HttpClient http, AppDbContext db)
    {
        _http = http;
        _db = db;
    }

    public async Task ImportBooksAsync(string query)
    {
        string url =
            $"https://www.googleapis.com/books/v1/volumes?q={query}";

        var json = await _http.GetStringAsync(url);

        var data = JsonSerializer.Deserialize<GoogleBooksResult>(json);

        if (data?.items == null || data.items.Count == 0)
            throw new Exception("Kitap bulunamadı");

        foreach (var item in data.items.Take(50))
        {
            var info = item.volumeInfo;

            var book = new Book
            {
                Title = info.title,
                Authors = info.authors != null ? string.Join(", ", info.authors) : "Unknown",
                Description = info.description,
                PageCount = info.pageCount ?? 0,
                Genres = info.categories != null ? string.Join(", ", info.categories) : "",
                ReleaseYear = info.publishedDate?.Substring(0, 4),
                CoverUrl = info.imageLinks?.thumbnail
            };

            // Duplicate kontrolü
            if (!_db.Books.Any(b => b.Title == book.Title))
                _db.Books.Add(book);
        }

        await _db.SaveChangesAsync();
    }

}
