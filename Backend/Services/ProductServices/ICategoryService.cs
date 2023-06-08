using App.Dtos;
using App.Models.ProductModel;

namespace App.Services.ProductServices
{
      public interface ICategoryService
      {
            IQueryable<Category> GetAll();
            Task SaveCategoryAsync(Category category);
            Task UpdateCategoryAsync(Category category);
            Task DeleteAsync(int id);
            Category? GetById(int id);
      }
}
