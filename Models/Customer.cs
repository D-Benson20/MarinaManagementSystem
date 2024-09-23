using System.ComponentModel.DataAnnotations;

namespace MarinaManagementSystem.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Postcode { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<Boat> Boats { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
