using System.ComponentModel.DataAnnotations;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace FreeTube.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string? Title { get; set; }
        
        [Required]
        [Display(Name = "Genre")]
        public byte? GenreId { get; set; }
        public Genre? Genre { get; set; }
        
        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }
        
        public DateTime? DateAdded { get; set; }
        
        [Required(ErrorMessage = "Number In Stock must be 1 - 20")]
        [Range(0, 20)]
        [Display(Name = "Number In Stock")]
        public byte? NumberInStock { get; set; }
      


    }
}
