using System.ComponentModel.DataAnnotations;
using FreeTube.Models;

namespace FreeTube.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Provide Customer Name")]
        [StringLength(255)]
        public string? Name { get; set; }

        [Min18YearsIfMember]
        public DateTime? Birthdate { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public byte? MembershipTypeId { get; set; }
    }
}
