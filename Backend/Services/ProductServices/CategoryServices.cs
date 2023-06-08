using AutoMapper;
using App.Models.ProductModel;
using App.Repositories;
using App.Dtos;

namespace App.Services.ProductServices
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

            public async Task DeleteAsync(int id)
            {
                  var existedCategory = _unitOfWork.CategoryRepository.All().FirstOrDefault(x => x.Id == id);
                  if (existedCategory == null)
                  {
                        throw new Exception($"Cannot find category {id}");
                  }
                  try
                  {
                        if (existedCategory.ChildCategories?.Count > 0)
                        {
                              foreach (var item in existedCategory.ChildCategories)
                              {
                                    if (existedCategory.ParentCategory != null)
                                    {
                                          item.ParentCategory = existedCategory.ParentCategory;
                                    }
                                    else
                                    {
                                          item.ParentCategory = null;
                                    }
                              }
                        }
                        _unitOfWork.CategoryRepository.Delete(existedCategory);
                        await _unitOfWork.CompleteAsync();
                  }
                  catch (Exception ex)
                  {
                        throw new Exception($"Something went wrong when delete category! /n message: {ex.Message}");
                  }
            }

            public IQueryable<Category> GetAll()
            {
                  var categories = _unitOfWork.CategoryRepository.All();
                  return categories;
            }

            public Category? GetById(int id)
            {
                  var category = _unitOfWork.CategoryRepository.Find(c => c.Id == id).FirstOrDefault();
                  return category;
            }

            public async Task SaveCategoryAsync(Category category)
            {
                  var existedCategory = _unitOfWork.CategoryRepository.Find(x => x.Name == category.Name);
                  if (existedCategory == null)
                  {
                        throw new Exception("Category name is existed!");
                  }
                  try
                  {
                        if (category.ParentCategoryId == -1) category.ParentCategoryId = null;
                        Category newCategory = new() { Name = category.Name, Description = category.Description, Slug = category.Slug, ParentCategoryId = category.ParentCategoryId };
                        var result = _unitOfWork.CategoryRepository.Add(newCategory);
                        await _unitOfWork.CompleteAsync();
                  }
                  catch (Exception)
                  {

                        throw;
                  }
            }

            public async Task UpdateCategoryAsync(Category category)
            {
                  var existedCategory = _unitOfWork.CategoryRepository.All().FirstOrDefault(x => x.Id == category.Id);
                  if (existedCategory == null)
                  {
                        throw new Exception("Category name is existed!");
                  }

                  try
                  {
                        if (category.ParentCategoryId == -1) category.ParentCategory = null;
                        existedCategory.Name = category.Name;
                        existedCategory.Description = category.Description;
                        existedCategory.ParentCategory = category.ParentCategory;
                        _unitOfWork.CategoryRepository.Update(existedCategory);
                        await _unitOfWork.CompleteAsync();

                  }
                  catch (Exception ex)
                  {

                        throw new Exception($"Some thing went wrong, please try again!, /n ex: {ex.Message}");
                  }
            }

      }
}
