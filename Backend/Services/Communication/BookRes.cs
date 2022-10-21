using Backend.Models.Products;
using Backend.Resources;
using Backend.Services.Communication;

namespace Backend
{
    public class BookRes : BaseResponse
    {
        public BookResource BookResource { get; private set; }  
        public BookRes(bool success, string message, Book book) : base(success, message)
        {
            if(book != null)
            {
                var categories = new List<CategoryResource>();
                foreach (var item in book.Categories)
                {
                    categories.Add(new CategoryResource() { Id = item.Id, Name = item.Name }) ;
                }
                var authors = new List<AuthorResource>();
                foreach (var item in book.Authors)
                {
                    authors.Add(new AuthorResource() { Id = item.Id, Name = item.Name });
                }
                BookResource = new BookResource()
                {
                    Name = book.Name,
                    Id = book.Id,
                    Categories = categories,
                    Authors = authors,
                };

            }
        }
        public BookRes(string message)  : this(false, message , null) { }

        public BookRes(Book book) : this(true, string.Empty, book) { }

        public BookRes(bool success) : this(success, string.Empty, null) { }
    }
}