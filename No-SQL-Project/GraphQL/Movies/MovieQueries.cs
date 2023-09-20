using MongoDB.Driver;
using No_SQL_Project.Models;
using No_SQL_Project.Services;

namespace No_SQL_Project.GraphQL.Movies;

public class Query
{
    public async Task<List<Movie>> GetMoviesAsync(MoviesService moviesService)
    {
        var result = await moviesService.GetMoviesAsync();

        return result;
    }   
}