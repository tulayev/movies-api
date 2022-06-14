using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
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
    }
}
