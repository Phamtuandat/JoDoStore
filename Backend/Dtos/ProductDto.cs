
using App.Models.ProductModel;

namespace App.Dtos
{
      public class ProductDto
      {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int Price { get; set; }
            public int SalePrice { get; set; }
            public string NormalizedName { get; set; }
            public DateTime CreateAt;
            public string Slug { get; set; }
            public List<ProductCategoryDto> productCategories { get; set; }
            public string Thumbnail { get; set; }
            public string[] ImagePaths { get; set; }
            public string[] Tags { get; set; }
            public string? Detail { get; set; }
            public IconDto Icon { get; set; }
      }
}
