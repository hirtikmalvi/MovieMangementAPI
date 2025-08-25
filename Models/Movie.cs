using MovieManagementAPI.CustomValidators;
using System.ComponentModel.DataAnnotations;

namespace MovieManagementAPI.Models
{
    public class Movie
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Title { get; set; }
        public string? Director { get; set; }
        [ValidateReleaseYear]
        public int ReleaseYear { get; set; }
        public string? Genre { get; set; }
        [Range(1,10)]
        public int Rating { get; set; }
    }
}
