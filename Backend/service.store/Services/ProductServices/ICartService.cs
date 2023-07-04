using App.Models.Identity;
using App.Dtos;

namespace App.Services.ProductServices
{
      public interface ICartService
      {
            Task AddItemAsync(int productId, int quantity, string userId);
            Task RemoveItemAsync(int productId,string userId);
            Task<CartDto> GetCart(string userId);
      }
}
