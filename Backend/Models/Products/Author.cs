using System.ComponentModel.DataAnnotations;

namespace Backend.Models.Products
{
    public class Author
    {
        public Author()
        {
            this.Books = new HashSet<Book>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public string Description { get; set; } = string.Empty;

        public ICollection<Book> Books { get; set; }
    }
}