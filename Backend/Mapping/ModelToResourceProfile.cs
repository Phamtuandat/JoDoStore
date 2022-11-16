using AutoMapper;
using Backend.Models.Identity;
using Backend.Models.Products;
using Backend.Resources;

namespace Backend.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>();
            CreateMap<SaveProductResource, ProductModel>();
            CreateMap<Category, CategoryResource>();
            CreateMap<SaveCategoryResource, Category>();
            CreateMap<BrandModel, BrandResource>();
            CreateMap<SaveAuthorResourceModel, BrandModel>();
            CreateMap<MediaResource, Media>();
            CreateMap<Media, MediaResource>();
            CreateMap<ProductModel, ProductResource>().ForMember(s => s.MediaResource, c => c.MapFrom(m => m.Media));
        }
    }
}
