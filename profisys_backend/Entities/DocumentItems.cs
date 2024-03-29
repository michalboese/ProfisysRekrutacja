using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace profisys_backend.Entities
{
    public class DocumentItems
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Documents")]
        public int DocumentId { get; set; }

        [Required]
        public int Ordinal { get; set; }

        [Required]
        [StringLength(50)]
        public string Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int TaxRate { get; set; }
    }
}
