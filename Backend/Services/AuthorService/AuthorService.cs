using Backend.Models.Products;
using Backend.Repositories;
using Backend.Services.Communication;

namespace Backend.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorService( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AuthorRes> DeleteAsync(int id)
        {
            var existtingAuthor = _unitOfWork.AuthorRepository.Get(id);
            if (existtingAuthor == null) return new AuthorRes("Author id is not already existed!");
            try
            {
                _unitOfWork.AuthorRepository.Delete(existtingAuthor);
                await _unitOfWork.CompleteAsync();
                return new AuthorRes(true);
            }catch(Exception ex)
            {
                return new AuthorRes($"Something went wrong when deleting the author, please try again later! /n {ex.Message} ");
            }
        }

        public  IEnumerable<Author> FindAuthor(string name)
        {
            var authors = _unitOfWork.AuthorRepository.Find(a => a.Name == name).ToList();
            return  authors;

        }

        public AuthorRes GetById(int id)
        {
            var existedAuthor =  _unitOfWork.AuthorRepository.Get(id);
            if (existedAuthor == null) return new AuthorRes("can't find author id");
            return new AuthorRes(existedAuthor);
        }

        public IEnumerable<Author> GetAll()
        {
            return  _unitOfWork.AuthorRepository.All();
        }

        public async Task<AuthorRes> SaveAsync(Author author)
        {
            
            var existedAuthor =  _unitOfWork.AuthorRepository.Find(a => a.Name == author.Name).FirstOrDefault();
            if (existedAuthor != null) return new AuthorRes("This author is already existed!");
            try
            {
                _unitOfWork.AuthorRepository.Add(author);
                await _unitOfWork.CompleteAsync();
                return new AuthorRes(author);
            }catch(Exception ex)
            {
                return new AuthorRes($"An error occurred when saving the author: {ex.Message}");
            }

        }


        public async Task<AuthorRes> UpdateAsync(Author author, int id)
        {
            var existtingAuthor =  _unitOfWork.AuthorRepository.Get(id);
            if (existtingAuthor == null) return new AuthorRes("The author is not already existed!");
            existtingAuthor.Name = author.Name;
            existtingAuthor.Description = author.Description;
            try
            {
                var updatedAuthor = _unitOfWork.AuthorRepository.Update(existtingAuthor);
                await _unitOfWork.CompleteAsync();
                return new AuthorRes(updatedAuthor);
            }catch(Exception ex)
            {
                return new AuthorRes($"An error occurred when updating the author: {ex.Message}");
            }
        }

      
    }
}
