
using App.Models.ProductModel;

namespace App.Dtos
{
      public class ProductDto
      {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public int Price { get; set; }
            public int SalePrice { get; set; }
            public string NormalizedName { get; set; } = string.Empty;
            public DateTime CreateAt;
            public string Slug { get; set; }
            public BrandDto Brand { get; set; }
            public ICollection<ProductCategory> ProductCategories { get; set; }

            public string Thumbnail { get; set; }
            public string[] ImagePaths { get; set; }
            public string[] Tags { get; set; }
      }
}
