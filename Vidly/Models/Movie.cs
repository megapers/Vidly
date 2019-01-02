using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public Genre Genre { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        public DateTime? DateAdded { get; set; }

        [Display(Name = "Number In Stock")]
        [Range(1, 20)]
        public byte InStock { get; set; }

        [Display(Name = "Number Available")]
        [Range(1, 20)]
        public byte Available { get; set; }
        
    }
}