namespace Application.Interfaces;

public interface IMovieImportService
{
    Task ImportMovieAsync(string query);
    Task ImportPopularAsync();

}
