namespace App.Models.ProductModel
{
      public class Brand
      {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public virtual ICollection<Product>? Products { get; set; }
      }
}
