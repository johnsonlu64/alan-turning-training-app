using System.Collections.ObjectModel;

namespace MovieExtravaganza2.Model;

public interface IUserRepository
{

    public ObservableCollection<Movie> Movies { get; }
    public Task SelectAllMovies();

    public Task<Movie?> SelectMovie(String imdbId);
    public Task<MovieAdditionError> InsertMovie(Movie movie);
    public Task<MovieEditError> UpdateMovie(Movie currentMovie, String title, int year, int numStars);
    public Task<MovieDeletionError> DeleteMovie(Movie movieToDelete);
}