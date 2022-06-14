namespace MoviesAPI.Models
{
    public class CinemaHall
    {
        public int Id { get; set; }

        public CinemaHallType CinemaHallType { get; set; }

        public decimal Cost { get; set; }

        public int CinemaId { get; set; }

        public Cinema Cinema { get; set; }

        public List<Movie> Movies { get; set; }
    }

    public enum CinemaHallType
    {
        TwoDimensions = 1,
        ThreeDimensions = 2,
        CXC = 3
    }
}
