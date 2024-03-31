using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace profisys_backend.Entities
{
    public class Documents
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Index(0)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Index(1)]
        public string Type { get; set; } = null!;

        [Required]
        [Index(2)]
        public DateOnly Date { get; set; }

        [Required]
        [StringLength(50)]
        [Index(3)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [Index(4)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [Index(5)]
        public string City { get; set; } = null!;

    }
}
