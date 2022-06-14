using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.DTO;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public MovieController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            var movie = await _db.Movies
                .Where(m => m.Id == id)
                .Include(m => m.Genres)
                .Include(m => m.CinemaHalls)
                    .ThenInclude(ch => ch.Cinema)
                .Include(m => m.ActorsMovies)
                    .ThenInclude(am => am.Actor)
                .Select(m => new MovieDTO
                {
                    Id = id,
                    Title = m.Title,
                    Genres = m.Genres.Select(g => new GenreDTO
                    {
                        Id = g.Id,
                        Name = g.Name,
                    }).ToList(),
                    Cinemas = m.CinemaHalls.Select(ch => new CinemaDTO
                    {
                        Id = ch.Cinema.Id,
                        Name = ch.Cinema.Name
                    }).ToList(),
                    Actors = m.ActorsMovies.Select(am => new ActorDTO
                    {
                        Id = am.Actor.Id,
                        Name = am.Actor.Name,
                        DateOfBirth = am.Actor.DateOfBirth
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (movie == null)
            {
                return NotFound();
            }

            movie.Cinemas = movie.Cinemas.DistinctBy(c => c.Id).ToList();

            return movie;
        }
    }
}