using Backend.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Identity;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Email { get; set; } 

    [MaxLength(15)]
    public int? Phone { get; set; }

    public byte[] PasswordHash { get; set; }

    public byte[] PasswordSalt { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    public virtual string Role { get; set; } = "Admin";

    public DateTime DateTime { get; set; } = DateTime.Now;

    public ICollection<Adress> AdressList { get; set; } = new List<Adress>();

}
