﻿using System.ComponentModel.DataAnnotations;

namespace FreeTube.Models
{
    public class Genre
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; } = null!;
    }
}
