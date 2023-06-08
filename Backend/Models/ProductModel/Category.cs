using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.ProductModel
{
      public class Category
      {
            public int Id { get; set; }
            [Required]
            [MaxLength(100)]
            public string Name { get; set; }
            public string? Description { get; set; }
            public virtual ICollection<ProductCategory>? ProductCategories { get; set; }
            public string Slug { get; set; }
            [Display(Name = "Parent Category")]
            public int? ParentCategoryId { get; set; }

            [ForeignKey("ParentCategoryId")]
            [Display(Name = "Parent Category")]
            public Category? ParentCategory { set; get; }
            public ICollection<Category>? ChildCategories { get; set; }
      }
      public class CategorySelecItem : Category
      {
            public int level { get; set; }
      }
}
