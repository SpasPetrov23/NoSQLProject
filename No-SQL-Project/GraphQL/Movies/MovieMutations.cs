using No_SQL_Project.Models;
using No_SQL_Project.Services;
using No_SQL_Project.Utils;

namespace No_SQL_Project.GraphQL.Movies;

public class Mutation
{
    public async Task<Movie> CreateMovie(MoviesService moviesService, Movie inputMovie)
    {
        var newMovie = new Movie
        {
            Runtime = inputMovie.Runtime,
            Title = inputMovie.Title,
            Year = inputMovie.Year,
            Rating = inputMovie.Rating,
            Cast = inputMovie.Cast,
            Directors = inputMovie.Directors,
            Genres = inputMovie.Genres
        };
        
        moviesService.CreateMovieAsync(newMovie);

        return newMovie;
    }

    public async Task UpdateMovie(MoviesService moviesService, string id, Movie inputMovie)
    {
        Movie movie = await moviesService.GetMovieAsync(id);

        if (movie is null) throw new AppException(ErrorMessages.MOVIE_NOT_FOUND);

        inputMovie.Id = movie.Id;

        await moviesService.UpdateAsync(id, inputMovie);
    }
}