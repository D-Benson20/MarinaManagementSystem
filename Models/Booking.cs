using System.ComponentModel.DataAnnotations;

namespace MarinaManagementSystem.Models
{
    public class Booking
    {
        public int BookingId { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int BoatId { get; set; }
        public Boat? Boat { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string? Details { get; set; }
    }

}