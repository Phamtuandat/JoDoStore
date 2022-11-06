using Backend.Models.Products;
using Backend.Resources;

namespace Backend.Services.Product
{
    public interface IProductService
    {
        IQueryable<ProductModel> GetAll();
        Task<ProductRes> CreateAsync(SaveProductResource model);
        Task<ProductRes> UpdateAsync(SaveProductResource book, int id);
        Task<ProductRes> DeletAsync(int id);
        Task<ProductRes> GetById(int id);
    }
}
