using System.ComponentModel.DataAnnotations;

namespace Backend
{
    public class RegisterRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        [Required]
        public int PhoneNumber { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        

    }
}