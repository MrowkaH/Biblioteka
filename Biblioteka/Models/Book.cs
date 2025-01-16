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
        public string Tytul { get; set; }

        [Required]
        [StringLength(100)]
        public string Autor { get; set; }

        [Required]
        [StringLength(13)]
        public string ISBN { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Ilość kopii musi być nieujemna.")]
        public int DostepneKopie { get; set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}
