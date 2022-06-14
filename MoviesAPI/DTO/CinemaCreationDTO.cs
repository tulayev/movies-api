namespace MoviesAPI.DTO
{
    public class CinemaCreationDTO
    {
        public string Nmae { get; set; }

        public double Latitude { get; set; }
        
        public double Longtitude { get; set; }

        public CinemaOfferCreationDTO CinemaOffer { get; set; }
        
        public List<CinemaHallCreationDTO> CinemaHalls { get; set; }
    }
}
