using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Vidly.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }

        public GenreDto Genre { get; set; }

        [Display(Name = "Number In Stock")]
        [Range(1, 20)]
        public byte InStock { get; set; }

        [Display(Name = "Number Available")]
        [Range(1, 20)]
        public byte Available { get; set; }
    }
}
