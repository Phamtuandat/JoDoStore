using AutoMapper;
using gearshop_dotnetapp.Models.ProductModel;
using gearshop_dotnetapp.Repositories;
using gearshop_dotnetapp.Resources;
using Microsoft.EntityFrameworkCore;

namespace gearshop_dotnetapp.Services.ProductServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryRes> DeleteAsync(int id)
        {
            var existedCategory = _unitOfWork.CategoryRepository.All().FirstOrDefault(x => x.Id == id);
            if (existedCategory == null)
            {
                return new CategoryRes("Category name is existed!");
            }
            try
            {
                _unitOfWork.CategoryRepository.Delete(existedCategory);
                await _unitOfWork.CompleteAsync();
                return new CategoryRes(true);
            }
            catch (Exception ex)
            {
                return new CategoryRes($"Something went wrong when delete category! /n message: {ex.Message}");
            }
        }

        public async Task<IEnumerable<CategoryResource>> GetAll()
        {
            var categories = await _unitOfWork.CategoryRepository.All().ToListAsync();
            var result  = _mapper.Map<ICollection<Category>, ICollection<CategoryResource>>(categories);
            return result;
        }

        public CategoryRes GetById(int id)
        {
            var category = _unitOfWork.CategoryRepository.All().FirstOrDefault(x => x.Id == id);
            if (category == null) return new CategoryRes("can not find category");
            var result = _mapper.Map<Category, CategoryResource>(category);
            return new CategoryRes(result);
        }

        public async Task<CategoryRes> SaveCategoryAsync(SaveCategoryResource category)
        {
            var existedCategory =  _unitOfWork.CategoryRepository.Find(x => x.Name == category.Name);
            if (existedCategory == null)
            {
                return new CategoryRes("Category name is existed!");
            }
            try
            {
                Category newCategory = new() { Name = category.Name, Description = category.Description };
                var result = _unitOfWork.CategoryRepository.Add(newCategory);
                var categoryRes = _mapper.Map<Category, CategoryResource>(result);
                await _unitOfWork.CompleteAsync();
                return new CategoryRes(categoryRes);
            }
            catch (Exception ex)
            {

                return new CategoryRes($"Some thing went wrong, please try again!, /n ex: {ex.Message}");
            }
        }

        public async Task<CategoryRes> UpdateCategoryAsync(SaveCategoryResource category, int id)
        {
            var existedCategory = _unitOfWork.CategoryRepository.All().FirstOrDefault(x => x.Id == id) ;
            if (existedCategory == null)
            {
                return new CategoryRes("Category name is existed!");
            }

            try
            {
                existedCategory.Name = category.Name;
                existedCategory.Description = category.Description;
                _unitOfWork.CategoryRepository.Update(existedCategory);
                await _unitOfWork.CompleteAsync();

                return new CategoryRes(true);
            }
            catch (Exception ex)
            {

                return new CategoryRes($"Some thing went wrong, please try again!, /n ex: {ex.Message}");
            }
        }
    }
}
