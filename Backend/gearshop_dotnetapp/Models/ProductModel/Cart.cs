using gearshop_dotnetapp.Models.Identity;

namespace gearshop_dotnetapp.Models.ProductModel
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public virtual User? User { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
