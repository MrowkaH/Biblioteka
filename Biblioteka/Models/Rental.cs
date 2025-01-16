using System;
using System.ComponentModel.DataAnnotations;

namespace Biblioteka.Models
{
    public class Rental
    {
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public DateTime DueDate { get; set; }
    }
}
