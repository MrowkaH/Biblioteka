using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        [Required]
        [StringLength(13)]
        public string ISBN { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Number of copies must be non-negative.")]
        public int CopiesAvailable { get; set; } 
        public ICollection<Rental> Rentals { get; set; }
    }
}
