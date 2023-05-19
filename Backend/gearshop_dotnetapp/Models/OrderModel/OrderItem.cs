using gearshop_dotnetapp.Models.ProductModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace gearshop_dotnetapp.Models.OrderModel
{
    public class OrderItem
    {
        public int Id { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order Order { get; set; } = new Order();        
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = new Product();

        public decimal TotalPrice
        {
            get
            {
                return UnitPrice * Quantity;
            }
        }
    }
}
