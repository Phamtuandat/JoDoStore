using gearshop_dotnetapp.Enums;
using gearshop_dotnetapp.Models.OrderModel;

namespace gearshop_dotnetapp.Resources
{
    public class OrderResource
    {
        public int Id { get; set; }
        public ICollection<OrderItemResource> OrderItems { get; set; } = new List<OrderItemResource>();
        public decimal ShippingCash { get; set; }
        public decimal TotalPrice { get; set;}
        public decimal SubtotalPrice { get; set;}
        public AddressResource AddressBook { get; set; } = new AddressResource();
        public DateTime OrderDate { get; set; }
        public UserResource User { get; set; } = new UserResource();
        public string Status { get; set; } = string.Empty;
        public string UserName { get; set; }  = string.Empty;
    }
}
