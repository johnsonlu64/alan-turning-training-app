public class UserRepository
{

  public ObservableCollection<Movie> Movies { get; private set; } = new();

  public async Task SelectAllMovies()
    {
        Console.WriteLine("Hello from SeletAllMovies()");
        try
        {
            var response = await client.From<Movie>().Get();
            Movies.Clear();

            foreach (var movie in response.Models)
            {
                Movies.Add(movie);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching movies: {ex.Message}");
        }
    }

    public async Task<Movie?> SelectMovie(string imdbId)
    {
        var response = await client.From<Movie>().Where(movie => movie.ImdbId == imdbId).Get();
        if (response != null)
        {
            return response!.Models.FirstOrDefault();
        }
        return null;
    }

    public async Task<MovieAdditionError> InsertMovie(Movie movie)
    {
        try
        {
            var response = await client.From<Movie>().Insert(movie); // insert into the database
            if (response.Models.Any())
            {
                Movies.Add(response.Models.First()); // update the local cache [or maybe just movie
            }
            else
            {
                return MovieAdditionError.DuplicateMovieId;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Insert failed, {0}", ex);
            return ex.Message.Contains("already exists") ? MovieAdditionError.DuplicateMovieId : MovieAdditionError.DBAdditionError;
        }
        return MovieAdditionError.NoError;
    }

    public async Task<MovieEditError> UpdateMovie(Movie movieToUpdate, string title, int year, int numStars)
    {
        try
        {
            movieToUpdate.Title = title;
            movieToUpdate.Year = year;
            movieToUpdate.NumStars = numStars;
            var response = await client.From<Movie>().Update(movieToUpdate);
            if (response.Models.Any())
            {
                var updatedMovie = response.Models.First();
                var index = Movies.IndexOf(movieToUpdate);
                if (index >= 0)
                {
                    Movies[index] = updatedMovie; // adding, deleting, or in this case replacing an element in a collection triggers a property changed event so the bound collection view will be refreshed. Changing the properties of an existing object does not, unless those properties invoke OnPropertyChanged()
                }
            }
            else
            {
                return MovieEditError.MovieNotFound; // that's the only way this could fail normally - exceptions are caught below
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Update failed, {0}", ex);
            return MovieEditError.DBEditError;
        }
        return MovieEditError.NoError;
    }

    public async Task<MovieDeletionError> DeleteMovie(Movie movieToDelete)
    {
        try
        {
            var response = await client.From<Movie>().Delete(movieToDelete);
            if (response.Models.Any())
            {
                Console.WriteLine($"{Movies.Count}");
                Movies.Remove(movieToDelete); // requires Equals() method be implemented
                Console.WriteLine($"{Movies.Count}");
            }
            else
            {
                return MovieDeletionError.MovieNotFound;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Delete failed, {0}", ex);
            return MovieDeletionError.DBDeletionError;
        }
        return MovieDeletionError.NoError;
    }
}