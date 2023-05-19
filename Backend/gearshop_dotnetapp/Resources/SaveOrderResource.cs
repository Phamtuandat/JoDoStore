using Backend.Models.Identity;

namespace gearshop_dotnetapp.Resources
{
    public class SaveOrderResource
    {
        public List<SaveOrderItemResource> OrderItems { get; set; } = new List<SaveOrderItemResource>();
        public int AddressId { get;  set; }
        public decimal ShippingCash { get; set; }
    }
}
