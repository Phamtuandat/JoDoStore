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
            CreateMap<Category, CategoryResource>();
            CreateMap<SaveCategoryResource, Category>();
            CreateMap<BrandModel, BrandResource>();
            CreateMap<SaveAuthorResourceModel, BrandModel>();
            CreateMap<ProductModel, ProductResource>();
        }
    }
}
