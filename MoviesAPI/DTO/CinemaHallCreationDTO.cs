using MoviesAPI.Models;

namespace MoviesAPI.DTO
{
    public class CinemaHallCreationDTO
    {
        public decimal Cost { get; set; }

        public CinemaHallType CinemaHallType { get; set; }
    }
}
