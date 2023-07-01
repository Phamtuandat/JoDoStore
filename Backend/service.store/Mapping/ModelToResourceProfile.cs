using AutoMapper;
using App.Models.Identity;
using App.Models.OrderModel;
using App.Models.ProductModel;
using App.Dtos;
using App.Areas.Products.Models;
using App.Models;

namespace App.Mapping
{
      public class ModelToResourceProfile : Profile
      {
            public ModelToResourceProfile()
            {
                  CreateMap<User, UserResource>();
                  CreateMap<Category, CategoryDto>();
                  CreateMap<Product, ProductDto>();
                  CreateMap<OrderItem, OrderItemDto>();
                  CreateMap<Order, OrderDto>();
                  CreateMap<CartItem, CartItemResource>();
                  CreateMap<Cart, CartDto>();
                  CreateMap<ProductDto, EditProductViewModel>();
                  CreateMap<EditProductViewModel, Product>();
                  CreateMap<CreateProducViewModel, Product>();
                  CreateMap<ProductDto, EditProductViewModel>();
                  CreateMap<RegisterModel, User>();
                  CreateMap<Icon, CreateIconViewModel>();
                  CreateMap<ProductCategory, ProductCategoryDto>();
                  CreateMap<Icon, IconDto>();


            }
      }
}
