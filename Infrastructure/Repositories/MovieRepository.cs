using Domain.Entities;
using Application.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repository;

public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _context;

    public MovieRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Movie movie)
    {
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
    }
}
