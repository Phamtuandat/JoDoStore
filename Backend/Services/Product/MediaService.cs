using Backend.Models.Products;
using Backend.Resources;

namespace Backend.Services.Product
{
    public class MediaService : IMediaService
    {
        public Task<MediaResource> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Media> GetAll()
        {
            throw new NotImplementedException();
        }

        public MediaResource GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<MediaResource> SaveCategoryAsync(Media media)
        {
            throw new NotImplementedException();
        }

        public Task<MediaResource> UpdateCategoryAsync(Media media, int id)
        {
            throw new NotImplementedException();
        }
    }
}
