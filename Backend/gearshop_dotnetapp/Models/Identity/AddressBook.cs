using gearshop_dotnetapp.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Identity
{
    public class AddressBook
    {
        public int Id { get; set; }
        [MaxLength(50)]

        public string? District { get; set; }

        [MaxLength(50)]
        public string? Province { set; get; }
        [MaxLength(50)]
        public string Ward { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Address { get; set; }
        [ForeignKey("UserId")]
        public bool IsDefault { get; set; } = true;
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public virtual User User { get; set; } 

    }
}
