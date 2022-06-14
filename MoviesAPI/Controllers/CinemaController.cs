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

        [HttpPost]
        public async Task<ActionResult> Post(CinemaCreationDTO cinemaCreationDTO)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var cinema = new Cinema
            {
                Name = cinemaCreationDTO.Nmae,
                Location = geometryFactory.CreatePoint(new Coordinate(cinemaCreationDTO.Longtitude, cinemaCreationDTO.Latitude)),
                CinemaOffer = new CinemaOffer
                {
                    DiscountPercentage = cinemaCreationDTO.CinemaOffer.DiscountPercentage,
                    Begin = cinemaCreationDTO.CinemaOffer.Begin,
                    End = cinemaCreationDTO.CinemaOffer.End
                },
                CinemHalls = cinemaCreationDTO.CinemaHalls.Select(ch => new CinemaHall
                {
                    Cost = ch.Cost,
                    CinemaHallType = ch.CinemaHallType
                }).ToList()
            };

            _db.Cinemas.Add(cinema);
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
