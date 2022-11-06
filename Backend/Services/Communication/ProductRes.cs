using Backend.Models.Products;
using Backend.Resources;
using Backend.Services.Communication;

namespace Backend
{
    public class ProductRes : BaseResponse
    {
        public ProductResource BookResource { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ProductRes(bool success, string message, ProductModel product) : base(success, message)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            if(product != null)
            {
                var categories = new List<CategoryResource>();
                foreach (var item in product.Categories)
                {
                    categories.Add(new CategoryResource() { Id = item.Id, Name = item.Name }) ;
                }
                var brands = new List<BrandResource>();
                foreach (var item in product.Brands)
                {
                    brands.Add(new BrandResource() { Id = item.Id, Name = item.Name });
                }
                BookResource = new ProductResource()
                {
                    Name = product.Name,
                    Id = product.Id,
                    Categories = categories,
                    Brands = brands,
                    Description = product.Descriptions,
                    Price = product.Price,
                    PriceSale = product.PriceSale,
                    SmallImageLink = product.SmallImageLink,
                    Thumbnail   = product.Thumbnail

                };

            }
        }
        public ProductRes(string message)  : this(false, message , null) { }

        public ProductRes(ProductModel product) : this(true, string.Empty, product) { }

        public ProductRes(bool success) : this(success, string.Empty, null) { }
    }
}