using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Data;
using MoviesAPI.DTO;
using MoviesAPI.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/cinemas")]
    public class CinemaController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public CinemaController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IEnumerable<CinemaDTO>> Get()
        {
            return await _db.Cinemas
                .Select(c => new CinemaDTO { Id = c.Id, Name = c.Name, Longtitude = c.Location.Y, Latitude = c.Location.Y })
                .ToListAsync();
        }
        
        [HttpGet("closetome")]
        public async Task<ActionResult> Get(double latitude, double longtitude)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var myLocation = geometryFactory.CreatePoint(new Coordinate(longtitude, latitude));

            int maxDistance = 5000;

            var cinemas = await _db.Cinemas
                .OrderBy(c => c.Location.Distance(myLocation))
                .Where(c => c.Location.IsWithinDistance(myLocation, maxDistance))
                .Select(c => new
                {
                    Name = c.Name,
                    Distance = Math.Round(c.Location.Distance(myLocation))
                })
                .ToListAsync();

            return Ok(cinemas);
        }
    }
}
