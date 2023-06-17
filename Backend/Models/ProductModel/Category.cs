using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Enums;
namespace App.Models.ProductModel
{
      public class Category
      {
            public int Id { get; set; }
            [Required]
            [MaxLength(100)]
            public string Name { get; set; }
            public string? Description { get; set; }
            public string Slug { get; set; }
            [Display(Name = "Parent Category")]
            public int? ParentCategoryId { get; set; }

            [ForeignKey("ParentCategoryId")]
            [Display(Name = "Parent Category")]
            public Category? ParentCategory { set; get; }
            public ICollection<Category>? ChildCategories { get; set; }
            public string? Gender { get; set; }
            public virtual List<ProductCategory>? ProductCategories { get; set; }
      }
      public class CategorySelecItem : Category
      {
            public int level { get; set; }
      }
}
