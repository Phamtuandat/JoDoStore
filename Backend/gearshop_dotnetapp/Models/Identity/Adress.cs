using gearshop_dotnetapp.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Identity
{
    [Table("Address")]
    public class Adress
    {
        public int Id { get; set; }
        [MaxLength(50)]

        public string? City { get; set; }

        [MaxLength(50)]
        public string? State { set; get; }

        [MaxLength(100)]
        public string? StreetAddress { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
