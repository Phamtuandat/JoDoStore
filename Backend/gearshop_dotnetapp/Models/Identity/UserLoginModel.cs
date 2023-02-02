using System.ComponentModel.DataAnnotations;

public class UserLoginModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}