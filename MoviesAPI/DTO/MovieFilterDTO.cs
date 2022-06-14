namespace MoviesAPI.DTO
{
    public class MovieFilterDTO
    {
        public string Title { get; set; }

        public int GenreId { get; set; }

        public bool OnGoing { get; set; }

        public bool UpComingReleases { get; set; }
    }
}
