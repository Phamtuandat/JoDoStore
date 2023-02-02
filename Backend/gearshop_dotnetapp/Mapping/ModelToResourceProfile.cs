using AutoMapper;
using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Models.Product;
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>();
            CreateMap<Category, CategoryResource>();
            CreateMap<Product, ProductResource>();
            CreateMap<ImageCollections, ImageCollectionResource>();
            CreateMap<Photo, PhotoResource>();
            CreateMap<RegisterResource, User>();
            CreateMap<Brand, BrandResource>();
        }
    }
}
