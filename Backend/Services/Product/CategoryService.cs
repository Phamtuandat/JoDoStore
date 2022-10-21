using Backend.Models.Products;
using Backend.Repositories;
using Backend.Services.Communication;

namespace Backend.Services.Product
{

    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uniOfWork;
        public CategoryService( IUnitOfWork unitOfWork)
        {
            _uniOfWork = unitOfWork;
        }

        public async Task<CategoryRes> DeleteAsync(int id)
        {
            
           var existtingCategory =  _uniOfWork.CategoryRepository.Get(id);
           if (existtingCategory == null) 
                return new CategoryRes("can't find category by this id, please try again!");
            try
            {
                _uniOfWork.CategoryRepository.Delete(existtingCategory);
                await _uniOfWork.CompleteAsync();
                return new CategoryRes(true);
            }
            catch ( Exception ex )
            {
                return new CategoryRes($"An error occurred when Deleting the category: {ex.Message}");
            }
        }

        public   IQueryable<Category> GetAll()
        {
            return  _uniOfWork.CategoryRepository.All();
        }

        public Category GetById(int id)
        {
            return  _uniOfWork.CategoryRepository.Get(id);
        }

        public async Task<CategoryRes> SaveCategoryAsync(Category category)
        {
            var existtingCategory = _uniOfWork.CategoryRepository.Find(c => c.Name == category.Name).FirstOrDefault();
            if (existtingCategory != null) return new CategoryRes("This category is existed!");
            try
            {
                 _uniOfWork.CategoryRepository.Add(category);
                await _uniOfWork.CompleteAsync();
                return new CategoryRes(category);
            }catch( Exception ex)
            {
                return new CategoryRes($"An error occurred when updating the category: {ex.Message}");
            }
           
        }

        public async Task<CategoryRes> UpdateCategoryAsync(Category category, int id)
        {
            var existedCategory = _uniOfWork.CategoryRepository.Get(id);
            if (existedCategory == null) return new CategoryRes("Can not find category by this id, please try again!");
            existedCategory.Name = category.Name;
            try
            {
                var entiry =_uniOfWork.CategoryRepository.Update(existedCategory);
                await _uniOfWork.CompleteAsync();
                return new CategoryRes(entiry);
            }
            catch(Exception ex)
            {
                return new CategoryRes($"An error occurred when updating the category: {ex.Message}");
            }
        }

    }
}
