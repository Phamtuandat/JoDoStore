using App.Models.Identity;

namespace App.Models.ProductModel
{
      public class Cart
      {
            public int Id { get; set; }
            public string UserId { get; set; } = string.Empty;
            public virtual User User { get; set; } = new User();
            public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
      }
}
