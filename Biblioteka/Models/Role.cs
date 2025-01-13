﻿using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
