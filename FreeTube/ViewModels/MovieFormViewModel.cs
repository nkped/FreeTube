using System.ComponentModel.DataAnnotations;
using FreeTube.Models;

namespace FreeTube.ViewModels
{
    public class MovieFormViewModel
    {
        public IEnumerable<Genre>? Genres { get; set; }

        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        public string? Title { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte? GenreId { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Required(ErrorMessage = "Number In Stock must be 1 - 20")]
        [Range(0, 20)]
        [Display(Name = "Number In Stock")]
        public byte? NumberInStock { get; set; }

        public DateTime? DateAdded { get; set; }

        //public string Title
        //{
        //    get
        //    {
        //        return Id != 0 ? "Edit Movie" : "New Movie";
        //    }
        //}
        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movie movie)
        {
            Id = movie.Id;
            Title = movie.Title;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
            DateAdded = movie.DateAdded;
        }
    }
}
