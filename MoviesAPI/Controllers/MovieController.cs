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

        [HttpGet("selectloading/{id:int}")]
        public async Task<IActionResult> SelectLoading(int id)
        {
            var movieWithGenres = await _db.Movies.Select(m => new
            {
                Id = m.Id,
                Title = m.Title,
                Genres = m.Genres.Select(g => g.Name).OrderByDescending(n => n).ToList()
            }).FirstOrDefaultAsync(m => m.Id == id);

            return movieWithGenres == null ? NotFound() : Ok(movieWithGenres);
        }

        [HttpGet("explicitloading/{id:int}")]
        public async Task<ActionResult<MovieDTO>> ExplicitLoading(int id)
        {
            var movie = await _db.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            await _db.Entry(movie).Collection(p => p.Genres).LoadAsync();
            var genresCount = await _db.Entry(movie).Collection(p => p.Genres).Query().CountAsync();

            var movieDTO = new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Genres = movie.Genres.Select(g => new GenreDTO 
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList()
            };

            return Ok(new
            {
                Movie = movieDTO,
                GenresCount = genresCount
            });
        }

        [HttpGet("groupByCinema")]
        public async Task<ActionResult> GetGroupedByCinema()
        {
            var groupedMovies = await _db.Movies
                .GroupBy(m => m.OnGoing)
                .Select(g => new
                {
                    OnGoing = g.Key,
                    Count = g.Count(),
                    Movies = g.ToList()
                }).ToListAsync();

            return Ok(groupedMovies);
        }

        [HttpGet("groupByGenresCount")]
        public async Task<ActionResult> GetGroupedByGenresCount()
        {
            var groupedMovies = await _db.Movies
                .GroupBy(m => m.Genres.Count)
                .Select(g => new
                {
                    Count = g.Key,
                    Titles = g.Select(m => m.Title),
                    Genres = g.Select(m => m.Genres).SelectMany(a => a).Select(ge => ge.Name).Distinct()
                }).ToListAsync();
                

            return Ok(groupedMovies);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> Filter([FromQuery] MovieFilterDTO movieFilterDTO)
        {
            var moviesQueryable = _db.Movies.AsQueryable();

            if (!String.IsNullOrWhiteSpace(movieFilterDTO.Title))
            {
                moviesQueryable = moviesQueryable.Where(m => m.Title.Contains(movieFilterDTO.Title));
            }

            if (movieFilterDTO.OnGoing)
            {
                moviesQueryable = moviesQueryable.Where(m => m.OnGoing);
            }

            if (movieFilterDTO.UpComingReleases)
            {
                moviesQueryable = moviesQueryable.Where(m => m.ReleaseDate > DateTime.Today);
            }

            if (movieFilterDTO.GenreId != 0)
            {
                moviesQueryable = moviesQueryable.Where(m => m.Genres.Select(g => g.Id).Contains(movieFilterDTO.GenreId));
            }

            var movies = await moviesQueryable.Include(m => m.Genres).ToListAsync();

            return movies.Select(m => new MovieDTO
            {
                Id = m.Id,
                Title = m.Title,
                Genres = m.Genres.Select(g => new GenreDTO
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToList()
            }).ToList();
        }
    }
}