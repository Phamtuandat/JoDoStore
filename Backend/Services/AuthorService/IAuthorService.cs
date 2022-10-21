using Backend.Models.Products;
using Backend.Services.Communication;

namespace Backend.Services.AuthorService
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAll();
        IEnumerable<Author> FindAuthor(string name);
        AuthorRes GetById(int id);
        Task<AuthorRes> SaveAsync(Author author);
        Task<AuthorRes> UpdateAsync(Author author, int id);
        Task<AuthorRes> DeleteAsync(int id);
    }
}