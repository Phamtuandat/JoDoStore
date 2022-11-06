using Backend.Models.Products;
using Backend.Repositories;
using Backend.Services.Communication;

namespace Backend.Services.Brand
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BrandService( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BrandRes> DeleteAsync(int id)
        {
            var existtingAuthor = _unitOfWork.AuthorRepository.Get(id);
            if (existtingAuthor == null) return new BrandRes("Author id is not already existed!");
            try
            {
                _unitOfWork.AuthorRepository.Delete(existtingAuthor);
                await _unitOfWork.CompleteAsync();
                return new BrandRes(true);
            }catch(Exception ex)
            {
                return new BrandRes($"Something went wrong when deleting the author, please try again later! /n {ex.Message} ");
            }
        }

        public  IEnumerable<BrandModel>? FindAuthor(string name)
        {
            var brand = _unitOfWork.AuthorRepository.Find(a => a.Name == name)?.ToList();
            return  brand;

        }

        public BrandRes GetById(int id)
        {
            var existedAuthor =  _unitOfWork.AuthorRepository.Get(id);
            if (existedAuthor == null) return new BrandRes("can't find author id");
            return new BrandRes(existedAuthor);
        }

        public IEnumerable<Models.Products.BrandModel> GetAll()
        {
            return  _unitOfWork.AuthorRepository.All();
        }

        public async Task<BrandRes> SaveAsync(Models.Products.BrandModel brand)
        {
            
            var existedAuthor =  _unitOfWork.AuthorRepository.Find(a => a.Name == brand.Name)?.FirstOrDefault();
            if (existedAuthor != null) return new BrandRes("This author is already existed!");
            try
            {
                _unitOfWork.AuthorRepository.Add(brand);
                await _unitOfWork.CompleteAsync();
                return new BrandRes(brand);
            }catch(Exception ex)
            {
                return new BrandRes($"An error occurred when saving the author: {ex.Message}");
            }

        }


        public async Task<BrandRes> UpdateAsync(Models.Products.BrandModel author, int id)
        {
            var existtingAuthor =  _unitOfWork.AuthorRepository.Get(id);
            if (existtingAuthor == null) return new BrandRes("The author is not already existed!");
            existtingAuthor.Name = author.Name;
            existtingAuthor.Description = author.Description;
            try
            {
                var updatedAuthor = _unitOfWork.AuthorRepository.Update(existtingAuthor);
                await _unitOfWork.CompleteAsync();
                return new BrandRes(updatedAuthor);
            }catch(Exception ex)
            {
                return new BrandRes($"An error occurred when updating the author: {ex.Message}");
            }
        }

      
    }
}
