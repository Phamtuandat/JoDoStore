namespace Backend.Models.Products
{
    public class Book
    {
        public Book()
        {
            this.Categories = new HashSet<Category>();
            this.Authors = new HashSet<Author>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Author> Authors { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public string Descriptions { get; set; } = String.Empty;
        public int Price { get; set; }
        public int PriceSale { get; set; }
        public DateTime CreatedTimestamp { get; set; }
        public int PageCount { get; set; }
        public string SmallImageLink { get; set; } = String.Empty;
        public string Thumbnail { get; set; } = string.Empty;
    }
}