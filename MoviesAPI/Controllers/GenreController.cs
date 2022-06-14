using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.DTO;
using MoviesAPI.Models;
using MoviesAPI.Utils;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenreController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public GenreController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<Genre>> Get(int page, int perPage)
        {
            return await _db.Genres
                .AsNoTracking()
                .OrderBy(g => g.Name)
                .Paginate(page: page, perPage: perPage)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post(GenreCreationDTO genreDto)
        {
            var genre = new Genre
            {
                Name = genreDto.Name
            };
            _db.Genres.Add(genre);
            await _db.SaveChangesAsync();
            return Ok(genre);
        }

        [HttpPost("multiple")]
        public async Task<IActionResult> Post(GenreCreationDTO[] genreDtos)
        {
            var genres = genreDtos.Select(dto => new Genre { Name = dto.Name });
            _db.Genres.AddRange(genres);
            await _db.SaveChangesAsync();
            return Ok(genres);
        }
    }
}
