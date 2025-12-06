using System;
using System.Net.Http;
using System.Text.Json;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Services;

public class MovieImportService : IMovieImportService
{
    private readonly HttpClient _http;
    private readonly AppDbContext _db;
    private readonly string apiKey = "f072198893c01e8cb48813f73cdfe38b";

    public MovieImportService(HttpClient http, AppDbContext db)
    {
        _http = http;
        _db = db;
    }

    public async Task ImportMovieAsync(string query)
    {
        string searchUrl =
            $"https://api.themoviedb.org/3/search/movie?api_key={apiKey}&query={query}";

        var searchJson = await _http.GetStringAsync(searchUrl);
        var searchData = JsonSerializer.Deserialize<TmdbSearchResult>(searchJson);

        if (searchData.results.Count == 0)
            throw new Exception("Film bulunamadı");

        var basic = searchData.results[0];

        string detailUrl =
            $"https://api.themoviedb.org/3/movie/{basic.id}?api_key={apiKey}&append_to_response=credits";

        var detailJson = await _http.GetStringAsync(detailUrl);
        var detail = JsonSerializer.Deserialize<TmdbMovieDetail>(detailJson);

        string director = detail.credits.crew
            .FirstOrDefault(c => c.job == "Director")?.name ?? "Unknown";

        var actors = detail.credits.cast.Take(5).Select(c => c.name).ToList();
        var genres = detail.genres.Select(g => g.name).ToList();

        var movie = new Movie
        {
            Title = detail.title,
            Overview = detail.overview,
            ReleaseYear = detail.release_date?.Substring(0, 4),
            Director = director,
            Actors = string.Join(", ", actors),
            Genres = string.Join(", ", genres),
            PosterUrl = "https://image.tmdb.org/t/p/w500" + detail.poster_path
        };

        _db.Movies.Add(movie);
        await _db.SaveChangesAsync();
    }
    public async Task ImportPopularAsync()
    {
        string url =
            $"https://api.themoviedb.org/3/movie/popular?api_key={apiKey}&language=en-US&page=1";

        var json = await _http.GetStringAsync(url);

        var data = JsonSerializer.Deserialize<TmdbSearchResult>(json);

        int limit = 50;
        var moviesToImport = data.results.Take(limit).ToList();

        foreach (var basic in moviesToImport)
        {
            // detay bilgileri çek
            string detailUrl =
                $"https://api.themoviedb.org/3/movie/{basic.id}?api_key={apiKey}&append_to_response=credits";

            var detailJson = await _http.GetStringAsync(detailUrl);
            var detail = JsonSerializer.Deserialize<TmdbMovieDetail>(detailJson);

            string director = detail.credits.crew
                .FirstOrDefault(c => c.job == "Director")?.name ?? "Unknown";

            var actors = detail.credits.cast.Take(5).Select(c => c.name).ToList();
            var genres = detail.genres.Select(g => g.name).ToList();

            var movie = new Movie
            {
                Title = detail.title,
                Overview = detail.overview,
                ReleaseYear = detail.release_date?.Substring(0, 4),
                Director = director,
                Actors = string.Join(", ", actors),
                Genres = string.Join(", ", genres),
                PosterUrl = "https://image.tmdb.org/t/p/w500" + detail.poster_path
            };

            // Duplicate kontrolü
            if (!_db.Movies.Any(m => m.Title == movie.Title))
            {
                _db.Movies.Add(movie);
            }
        }

        await _db.SaveChangesAsync();
    }

}
