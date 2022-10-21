using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Products
{
    public class Category
    {
        public Category()
        {
            this.Books = new HashSet<Book>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(225)]
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
