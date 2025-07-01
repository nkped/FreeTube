using System.ComponentModel.DataAnnotations;

namespace FreeTube.Models
{
    public class Rental
    {
        public int Id { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
        
        [Required]
        public Customer Customer { get; set; } = null!;
        
        [Required]
        public Movie Movie { get; set; } = null!;
    }
}
