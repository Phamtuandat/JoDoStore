using Backend.Models.Identity;
using gearshop_dotnetapp.Enums;
using gearshop_dotnetapp.Models.Identity;

namespace gearshop_dotnetapp.Models.OrderModel
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public virtual User User { get; set; } = new User();
        public AddressBook AddressBook { get; set; } = new AddressBook();
        public decimal ShippingCash { get; set; } = 0;
        public decimal SubtotalPrice { get; set; }
        public OrderStatus Status { get; set; }
    }
}
