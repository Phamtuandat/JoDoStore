using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.ProductModel
{
      [Table("ProductCategory")]
      public class ProductCategory
      {
            public int ProductId { get; set; }

            public int CategoryId { get; set; }

            [ForeignKey("CategoryId")]
            public Category Category { get; set; }

            [ForeignKey("ProductId")]
            public Product Product { get; set; }
      }
}