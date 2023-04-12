using Backend.Models.Identity;
using gearshop_dotnetapp.Enums;
using gearshop_dotnetapp.Models.OrderModel;
using gearshop_dotnetapp.Models.ProductModel;
using Microsoft.AspNetCore.Identity;

namespace gearshop_dotnetapp.Models.Identity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public Gender Gender { get; set; }
        public virtual ICollection<AddressBook>? AddressBook { get; set; }
        public override string? UserName { get => base.UserName; set => base.UserName = value; }
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        public DateOnly Birthday { get; set; }
        public override string Email { get; set; } = string.Empty;
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        public virtual ICollection<Order>? Orders { get; set; }
        public virtual Cart? Cart { get; set; }
    }

}
