﻿using System.ComponentModel.DataAnnotations;

namespace FreeTube.Models
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Please Provide Customer Name")]
        [StringLength(255)]
        public string? Name { get; set; }

        [Min18YearsIfMember]
        [Display(Name = "Date of Birth")]
        public DateTime? Birthdate { get; set; }
        
        public bool IsSubscribedToNewsletter { get; set; }

        [Display(Name = "Membership Type")]
        public byte? MembershipTypeId { get; set; }
        
        public MembershipType? MembershipType { get; set; }
    }
}
