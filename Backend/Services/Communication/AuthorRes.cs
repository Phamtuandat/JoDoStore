using Backend.Models.Products;
using Backend.Resources;

namespace Backend.Services.Communication
{
    public class AuthorRes : BaseResponse
    {
        public AuthorResource AuthorResource { get; private set; }
        
        public AuthorRes(bool success, string message, Author author) : base(success, message) {
            if(author != null)
            {
                AuthorResource = new AuthorResource()
                {
                    Id = author.Id,
                    Name = author.Name,
                    Description = author.Description,
                };

            }
        }
        public AuthorRes(Author author) : this(true, string.Empty, author) { }
        public AuthorRes(string message) : this(false, message, null) { }
        public AuthorRes(bool success) : this(success, string.Empty, null) { }
    } 
}
