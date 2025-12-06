using Domain.Entities;

namespace Application.Interfaces;

public interface IMovieRepository
{
    Task AddAsync(Movie movie);
}
