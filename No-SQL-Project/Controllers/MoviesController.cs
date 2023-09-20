using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using No_SQL_Project.Models;
using No_SQL_Project.Services;

namespace No_SQL_Project.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class MoviesController : ControllerBase
{
    private readonly MoviesService _moviesService;

    public MoviesController(MoviesService moviesService)
    {
        _moviesService = moviesService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Movie>>> GetMovies()
    {
        List<Movie> movies = await _moviesService.GetMoviesAsync();

        return Ok(movies);
    }

    [HttpGet]
    public async Task<ActionResult<Movie>> Get(string id)
    {
        Movie movie = await _moviesService.GetMovieAsync(id);

        if (movie is null) return NotFound();

        return Ok(movie);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Movie newMovie)
    {
        await _moviesService.CreateMovieAsync(newMovie);

        return Ok(newMovie);
    }

    [HttpPut]
    public async Task<IActionResult> Update(string id, Movie inputMovie)
    {
        Movie movie = await _moviesService.GetMovieAsync(id);

        if (movie is null) return NotFound();

        inputMovie.Id = movie.Id;

        await _moviesService.UpdateAsync(id, inputMovie);

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        Movie movie = await _moviesService.GetMovieAsync(id);

        if (movie is null) return NotFound();

        await _moviesService.DeleteAsync(id);

        return NoContent();
    }
}