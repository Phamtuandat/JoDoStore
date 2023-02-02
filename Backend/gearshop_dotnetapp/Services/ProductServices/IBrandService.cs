using gearshop_dotnetapp.Resources;
using gearshop_dotnetapp.Services.Communications;

namespace gearshop_dotnetapp.Services.ProductServices
{
    public interface IBrandService
    {
        Task<BrandRes> CreateAsync(SaveBrandResource brand);
        Task<BrandRes> UpdateAsync(SaveBrandResource brand, int id);
        Task<BrandRes> DeleteAsync(int id);
        IEnumerable<BrandResource> GetAll();
        IEnumerable<BrandResource> FindByName(string name);
        BrandRes GetById(int id);
    }
}
