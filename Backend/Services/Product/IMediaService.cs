using Backend.Models.Products;
using Backend.Resources;

namespace Backend.Services.Product
{
    public interface IMediaService
    {
        IQueryable<Media> GetAll();
        Task<MediaResource> SaveCategoryAsync(Media media);
        Task<MediaResource> UpdateCategoryAsync(Media media, int id);
        Task<MediaResource> DeleteAsync(int id);
        MediaResource GetById(int id);
    }
}
