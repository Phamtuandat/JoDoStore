using gearshop_dotnetapp.Resources;
using gearshop_dotnetapp.Services.Communications;

namespace gearshop_dotnetapp.Services.ProductServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResource>> GetAll();
        Task<ProductRes> CreateAsync(SaveProductResource saveProductResource);
        Task<ProductRes> UpdateAsync(SaveProductResource saveProductResource, int id);
        Task<ProductRes> DeleteAsync(int id);
        ProductRes GetById(int id);
        IEnumerable<ProductResource> FindByName(string name);

    }
}
