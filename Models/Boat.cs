using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarinaManagementSystem.Models
{
    public class Boat
    {
        public int BoatId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Length { get; set; }

        public int CustomerId { get; set; }
    }
}
