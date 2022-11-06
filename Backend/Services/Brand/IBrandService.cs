using Backend.Models.Products;
using Backend.Services.Communication;

namespace Backend.Services.Brand
{
    public interface IBrandService
    {
        IEnumerable<BrandModel> GetAll();
        IEnumerable<BrandModel>? FindAuthor(string name);
        BrandRes GetById(int id);
        Task<BrandRes> SaveAsync(BrandModel author);
        Task<BrandRes> UpdateAsync(BrandModel author, int id);
        Task<BrandRes> DeleteAsync(int id);
    }
}