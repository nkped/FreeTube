﻿using System.ComponentModel.DataAnnotations;

namespace FreeTube.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime? Birthdate { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public byte MembershipTypeId { get; set; }
        public MembershipType MembershipType { get; set; }
    }
}
