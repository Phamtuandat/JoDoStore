using App.Models.ProductModel;
using App.Dtos;

namespace App.Services.ProductServices
{
      public interface IProductService
      {
            List<ProductDto> GetAllAsync();
            Task CreateAsync(Product product);
            Task UpdateAsync(Product product);
            Task DeleteAsync(int id);
            ProductDto? GetById(int id);
            IEnumerable<ProductDto> FindByName(string name);

      }
}
