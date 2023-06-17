using App.Models.ProductModel;

namespace App.Services.ProductServices
{
      public interface IIconService
      {
            List<Icon> GetAll();
            Task CreateAsync(Icon icon);
            Task UpdateAsync(Icon icon);
            Task DeleteAsync(int id);
            Icon? GetById(int id);
            IEnumerable<Icon> FindByName(string name);

      }
}
