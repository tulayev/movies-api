namespace MoviesAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool OnGoing { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Poster { get; set; }

        public List<Genre> Genres { get; set; }

        public List<CinemaHall> CinemaHalls { get; set; }

        public List<ActorMovie> ActorsMovies { get; set; }
    }
}
