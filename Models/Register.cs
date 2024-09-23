using System.ComponentModel.DataAnnotations;

namespace MarinaManagementSystem.Models
{
    public class Register
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Phone] // Optional field, make it required if necessary
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^[A-Z0-9]{5,10}$", ErrorMessage = "Invalid postcode format.")]
        public string Postcode { get; set; }
    }
}
