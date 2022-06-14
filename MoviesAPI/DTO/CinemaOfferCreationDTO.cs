using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTO
{
    public class CinemaOfferCreationDTO
    {
        [Range(1, 100)]
        public decimal DiscountPercentage { get; set; }

        public DateTime Begin { get; set; }
        
        public DateTime End { get; set; }
    }
}
