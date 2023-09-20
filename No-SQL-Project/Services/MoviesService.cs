using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using No_SQL_Project.Models;

namespace No_SQL_Project.Services;

public class MoviesService
{
    private readonly IMongoCollection<Movie> _moviesCollection;

    public MoviesService(IOptions<MoviesDatabaseSettings> movieDbSettigs)
    {
        MongoClient mongoClient = new MongoClient(
            movieDbSettigs.Value.ConnectionString);

        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(
            movieDbSettigs.Value.DatabaseName);

        _moviesCollection = mongoDatabase.GetCollection<Movie>(movieDbSettigs.Value.MoviesCollectionName);
    }

    public async Task<List<Movie>> GetMoviesAsync()
    {
        List<Movie> result = await _moviesCollection.Find(_ => true).ToListAsync();

        return result;
    }

    public async Task<Movie?> GetMovieAsync(string id)
    {
        Movie result = await _moviesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        return result;
    }

    public async Task CreateMovieAsync(Movie newMovie)
    {
        await _moviesCollection.InsertOneAsync(newMovie);
    }

    public async Task UpdateAsync(string id, Movie inputMovie)
    {
        await _moviesCollection.ReplaceOneAsync(x => x.Id == id, inputMovie);
    }

    public async Task DeleteAsync(string id)
    {
        await _moviesCollection.DeleteOneAsync(x => x.Id == id);
    }
}