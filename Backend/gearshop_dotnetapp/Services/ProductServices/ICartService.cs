using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp.Services.ProductServices
{
    public interface ICartService
    {
        Task AddItemAsync(int productId, int quantity, User user);
        Task RemoveItemAsync(int id, User user);
        Task<CartResource> GetCart(User user);
    }
}
