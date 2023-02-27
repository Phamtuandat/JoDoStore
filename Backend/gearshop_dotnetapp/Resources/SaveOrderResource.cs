using Backend.Models.Identity;

namespace gearshop_dotnetapp.Resources
{
    public class SaveOrderResource
    {
        public List<SaveOrderItemResource> OrderItems { get; set; }
        public int AddressId { get;  set; }
    }
}
