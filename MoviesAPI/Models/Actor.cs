namespace MoviesAPI.Models
{
    public class Actor
    {
        public int Id { get; set; }

        private string _name;

        public string Name 
        { 
            get { return _name; }
            set
            {
                // bEn AffLEk => Ben Afflek
                _name = String.Join(' ', value.Split(' ').Select(n => n[0].ToString().ToUpper() + n.Substring(1).ToLower()).ToArray());
            }
        }

        public string Biography { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public List<ActorMovie> ActorsMovies { get; set; }
    }
}
