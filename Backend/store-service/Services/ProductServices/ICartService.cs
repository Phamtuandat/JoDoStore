using App.Models.Identity;
using App.Dtos;

namespace App.Services.ProductServices
{
      public interface ICartService
      {
            Task AddItemAsync(int productId, int quantity, User user);
            Task RemoveItemAsync(int id, User user);
            Task<CartDto> GetCart(User user);
      }
}
