using System.ComponentModel.DataAnnotations;

namespace App.Areas.Products.Models
{
      public class CreateProducViewModel
      {
            [Required]
            [StringLength(160, MinimumLength = 5)]
            public string Name { set; get; } = string.Empty;
            [Display(Name = "Categories")]
            public int[] CategoryIDs { get; set; }
            [StringLength(160, MinimumLength = 5)]
            [RegularExpression(@"^[a-z0-9-]*$")]
            public string? Slug { set; get; }
            public string? Description { set; get; }
            public int SalePrice { get; set; }
            public int Price { get; set; }
            public string[] Tags { set; get; }
            public string[] ImagePaths { set; get; }
            public int BrandId { get; set; }
            public string Thumbnail { set; get; }
            public int IconId { set; get; }
            public string Technology { set; get; }
            public string? Detail { get; set; }
      }
}