using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.ProductModel
{
      public class Product
      {
            public int Id { get; set; }
            [Required]
            public string Name { get; set; } = string.Empty;
            public string NormalizedName { set; get; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string Thumbnail { get; set; }
            public string[] ImagePaths { get; set; }
            public string[] Tags { get; set; }
            [Display(Name = "Url")]
            public string Slug { get; set; }
            public int Price { get; set; }
            public int SalePrice { get; set; }
            public DateTime CreateAt { get; set; }
            public virtual List<ProductCategory> ProductCategories { get; set; }
            public int IconId { get; set; }
            [ForeignKey("IconId")]
            public Icon Icon { get; set; }
            public DateTime UpdateAt { get; set; }
            public string Technology { get; set; }
      }
}
