using gearshop_dotnetapp.Models.ProductModel;
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp.Services.ProductServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResource>> GetAll();
        Task<CategoryRes> SaveCategoryAsync(SaveCategoryResource category);
        Task<CategoryRes> UpdateCategoryAsync(SaveCategoryResource category, int id);
        Task<CategoryRes> DeleteAsync(int id);
        CategoryRes GetById(int id);
    }
}
