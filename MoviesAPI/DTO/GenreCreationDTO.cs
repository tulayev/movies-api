using System.ComponentModel.DataAnnotations;

namespace MoviesAPI.DTO
{
    public class GenreCreationDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
