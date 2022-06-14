using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.DTO;
using MoviesAPI.Models;
using MoviesAPI.Utils;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/actors")]
    public class ActorController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ActorController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<ActorDTO>> Get(int page, int perPage)
        {
            return await _db.Actors
                .AsNoTracking()
                .OrderBy(a => a.Name)
                .Select(a => new ActorDTO { Id = a.Id, Name = a.Name, DateOfBirth = a.DateOfBirth }) // send to frontend only required data like in laravel resources
                .Paginate(page: page, perPage: perPage)
                .ToListAsync();
        }

        [HttpGet("ids")]
        public async Task<IEnumerable<int>> GetIds()
        {
            return await _db.Actors.Select(a => a.Id).ToListAsync();
        }
    }
}
