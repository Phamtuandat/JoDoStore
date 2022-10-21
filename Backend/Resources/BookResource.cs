using Backend.Models.Products;

namespace Backend.Resources
{
    public class BookResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryResource> Categories { get; set; }
        public ICollection<AuthorResource> Authors { get; set; }
        

    }
}
