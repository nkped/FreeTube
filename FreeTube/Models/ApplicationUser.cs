using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace FreeTube.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string? DrivingLicense { get; set; }

        public ApplicationUser() { }
    }
}
