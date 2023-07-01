using System.ComponentModel.DataAnnotations;

namespace App.Areas.Contacts.Models
{

      public class Contact
      {
            public int Id { get; set; }

            [Display(Name = "Full Name")]
            [Required(ErrorMessage = "Full Name is required")]
            [StringLength(50)]
            public string FullName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Email is required")]
            [StringLength(50)]
            public string Email { get; set; } = string.Empty;
            [Required]
            [StringLength(50)]
            public string Phone { get; set; } = string.Empty;
            public string Message { get; set; } = string.Empty;
            public DateTime SentAt { get; set; }
      }
}