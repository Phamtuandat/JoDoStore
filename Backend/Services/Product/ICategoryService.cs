using Backend.Models.Products;
using Backend.Services.Communication;

namespace Backend.Services.Product
{
    public interface ICategoryService
    {
        IQueryable<Category> GetAll();
        Task<CategoryRes> SaveCategoryAsync(Category category);
        Task<CategoryRes> UpdateCategoryAsync(Category category, int id);
        Task<CategoryRes> DeleteAsync(int id);
        CategoryRes GetById(int id);
    }
}
