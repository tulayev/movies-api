namespace MoviesAPI.DTO
{
    public class MovieCreationDTO
    {
        public string Title { get; set; }
       
        public bool OnGoing { get; set; }

        public DateTime ReleaseDate { get; set; }

        public List<int> GenreIds { get; set; }
        
        public List<int> CinemaHallIds { get; set; }
        
        public List<ActorMovieCreationDTO> ActorsMovies { get; set; }
    }
}
