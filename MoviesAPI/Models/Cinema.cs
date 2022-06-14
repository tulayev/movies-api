using NetTopologySuite.Geometries;

namespace MoviesAPI.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public Point Location { get; set; }

        public CinemaOffer CinemaOffer { get; set; }

        public List<CinemaHall> CinemHalls { get; set; }
    }
}
