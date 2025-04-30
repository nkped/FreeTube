using System.ComponentModel.DataAnnotations;

namespace FreeTube.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please select Name")]
        [StringLength(255)]
        public string? Name { get; set; }

        [Min18YearsIfMember]
        [Display(Name = "Date of Birth")]
        public DateTime? Birthdate { get; set; }
        
        public bool IsSubscribedToNewsletter { get; set; }

        [Required(ErrorMessage = "Please select Membership Type")]
        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }
        
        public MembershipType? MembershipType { get; set; }
    }
}
