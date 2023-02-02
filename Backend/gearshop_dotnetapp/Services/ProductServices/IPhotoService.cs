using gearshop_dotnetapp.Models.Product;
using gearshop_dotnetapp.Resources;
using gearshop_dotnetapp.Services.Communications;

namespace gearshop_dotnetapp.Services.ProductServices
{
    public interface IPhotoService
    {
        Task<PhotoResponse> CreateAsync(SavPhotoResource model);
        Task<PhotoResponse> UpdateAsync(SavPhotoResource model, int id);
        Task<PhotoResponse> DeleteAsync(int id);
        PhotoResponse GetThumbnailById(int id);
        Task<IEnumerable<PhotoResource>> GetAllThumbnails();
    }
}
