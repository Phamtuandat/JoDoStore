using Backend.Models.Identity;
using gearshop_dotnetapp.Models.Identity;

namespace gearshop_dotnetapp.Models.OrderModel
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public virtual User User { get; set; }
        public Address Adress { get; set; }
    }
}
