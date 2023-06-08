using App.Dtos;
using App.Models.ProductModel;

namespace App.Services.ProductServices
{
      public interface IBrandService
      {
            Task CreateAsync(Brand brand);
            Task UpdateAsync(Brand brand, int id);
            Task DeleteAsync(int id);
            IEnumerable<BrandDto> GetAll();
            IEnumerable<BrandDto> FindByName(string name);
            BrandDto GetById(int id);
      }
}
