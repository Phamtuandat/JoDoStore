using AutoMapper;
using Backend.Models.Identity;
using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Models.OrderModel;
using gearshop_dotnetapp.Models.ProductModel;
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<User, UserResource>().ForMember(dest => dest.Gender, opt => opt.MapFrom<GenderValueResolver>());
            CreateMap<Category, CategoryResource>();
            CreateMap<Product, ProductResource>();
            CreateMap<ImageCollections, ImageCollectionResource>();
            CreateMap<Photo, PhotoResource>();
            CreateMap<RegisterResource, User>();
            CreateMap<Brand, BrandResource>();
            CreateMap<AddressBook, AddressResource>();
            CreateMap<OrderItem, OrderItemResource>();
            CreateMap<Order, OrderResource>().ForMember(dest => dest.Status, opt => opt.MapFrom<OrderValueResolver>()).ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FirstName + " " + src.User.LastName));

        }
    }
}
