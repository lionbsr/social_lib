// Domain/Entities/Content.cs
using System.Collections.Generic;
using System.Net.Mime;
using Domain.Enums;

namespace Domain.Entities;

public sealed class Content
{
    public long Id { get; set; }
    public ContentKind Type { get; set; }
    public string Title { get; set; } = null!;
    public string? OriginalTitle { get; set; }
    public string? Overview { get; set; }
    public int? Year { get; set; }
    public int? RuntimeOrPages { get; set; } // runtime for movies, pagecount for books
    public string? PosterUrl { get; set; }
    public string ExternalId { get; set; } = null!;
    public string ExternalSource { get; set; } = null!; // "TMDb", "GoogleBooks", "OpenLibrary", etc.
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;

    // Relations
    public ICollection<ContentGenre> ContentGenres { get; set; } = new List<ContentGenre>();
    public ICollection<PersonRole> PersonRoles { get; set; } = new List<PersonRole>();
    public ICollection<LibraryEntry> LibraryEntries { get; set; } = new List<LibraryEntry>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<ListItem> ListItems { get; set; } = new List<ListItem>();
}
