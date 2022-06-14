namespace MoviesAPI.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }
        
        public string Title { get; set; }

        public List<GenreDTO> Genres { get; set; }
        public List<CinemaDTO> Cinemas { get; set; }
        public List<ActorDTO> Actors { get; set; }
    }
}
