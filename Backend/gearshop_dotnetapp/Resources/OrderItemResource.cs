using gearshop_dotnetapp.Models.ProductModel;
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp
{
    public class OrderItemResource
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public int ProductId { get; set; }
    }
}