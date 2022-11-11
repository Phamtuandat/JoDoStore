using Backend.Models.Products;
using Backend.Resources;
using Backend.Services.Communication;

namespace Backend
{
    public class ProductRes : BaseResponse
    {
        public ProductResource ProductResource { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ProductRes(bool success, string message, ProductModel product) : base(success, message)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            if(product != null)
            {
                var brand = new BrandResource() { Name = product.Brand.Name, Id = product.Brand.Id };
                var categories = new List<CategoryResource>();
                foreach (var item in product.Categories)
                {
                    categories.Add(new CategoryResource() { Id = item.Id, Name = item.Name });
                }
                var thumbnailList = new List<MediaResource>();
                foreach (var item in product.Media)
                {
                    thumbnailList.Add(new MediaResource(item));
                }


                ProductResource = new ProductResource()
                {
                    Name = product.Name,
                    Id = product.Id,
                    Categories = categories,
                    Brand = brand,
                    Description = product.Descriptions,
                    Price = product.Price,
                    PriceSale = product.PriceSale,
                    SmallImageLink = product.SmallImageLink,
                    MediaResources = thumbnailList 

                };

            }
        }
        public ProductRes(string message)  : this(false, message , null) { }

        public ProductRes(ProductModel product) : this(true, string.Empty, product) { }

        public ProductRes(bool success) : this(success, string.Empty, null) { }
    }
}