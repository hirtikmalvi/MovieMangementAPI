using MovieManagementAPI.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace MovieManagementAPI.DTOs.Movie
{
    public class CreateMovieDTO
    {
        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        public string? Director { get; set; }
        [ValidateReleaseYear(ErrorMessage = "ReleaseYear must be >= 1000 and <= CurrentYear.")]
        public int ReleaseYear { get; set; }
        public string? Genre { get; set; }
        [Range(1, 10, ErrorMessage = "Rating must be between 1-10.")]
        public int Rating { get; set; }
    }
}
