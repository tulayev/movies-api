namespace MoviesAPI.Models
{
    public class Actor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Biography { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public List<ActorMovie> ActorsMovies { get; set; }
    }
}
