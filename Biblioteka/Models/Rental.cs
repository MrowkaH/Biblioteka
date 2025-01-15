using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class Rental
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public DateTime RentalDate { get; set; }

        public DateTime? ReturnDate { get; set; }
    }
}
