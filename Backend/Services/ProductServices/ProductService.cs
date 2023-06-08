using AutoMapper;
using App.Dtos;
using App.Models.ProductModel;
using App.Repositories;
using Microsoft.EntityFrameworkCore;

namespace App.Services.ProductServices
{
      public class ProductService : IProductService
      {
            private readonly ILogger<ProductService> _logger;
            private readonly IMapper _mapper;
            private readonly IUnitOfWork _unitOfWork;

            public ProductService(ILogger<ProductService> logger, IUnitOfWork unitOfWork, IMapper mapper)
            {
                  _logger = logger;
                  _unitOfWork = unitOfWork;
                  _mapper = mapper;
            }

            public async Task CreateAsync(Product product)
            {
                  try
                  {
                        // check if product already exists  in the database throw exception 
                        var isExist = _unitOfWork.ProductRepository.Find(p => p.Slug == product.Slug)?.FirstOrDefault();
                        if (isExist != null) throw new Exception("Product already exists in the database");
                        product.CreateAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                        product.UpdateAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                        _unitOfWork.ProductRepository.Add(product);
                        await _unitOfWork.CompleteAsync();
                  }
                  catch (Exception)
                  {

                        throw;
                  }
            }

            public async Task DeleteAsync(int id)
            {
                  try
                  {
                        // check if product already exists  in the database throw exception 
                        var isExist = _unitOfWork.ProductRepository.Find(p => p.Id == id)?.FirstOrDefault();
                        if (isExist == null) throw new Exception("Product do not already exists in the database");
                        _unitOfWork.ProductRepository.Delete(isExist);
                        await _unitOfWork.CompleteAsync();
                  }
                  catch (Exception)
                  {

                        throw;
                  }
            }

            public IEnumerable<ProductDto> FindByName(string name)
            {
                  return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(_unitOfWork.ProductRepository.Find(p => p.Name == name));
            }

            public List<ProductDto> GetAllAsync()
            {
                  List<Product> products = _unitOfWork.ProductRepository
                                                                  .All()
                                                                  .Include(p => p.Brand)
                                                                  .Include(p => p.ProductCategories)
                                                                  .ThenInclude(pc => pc.Category)
                                                                  .ToList();
                  var productList = _mapper.Map<List<Product>, List<ProductDto>>(products);

                  return productList;
            }

            public ProductDto? GetById(int id)
            {
                  return _mapper.Map<Product, ProductDto>(_unitOfWork.ProductRepository.Get(id));
            }

            public async Task UpdateAsync(Product product)
            {
                  try
                  {
                        product.UpdateAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
                        _unitOfWork.ProductRepository.Update(product);
                        await _unitOfWork.CompleteAsync();
                  }
                  catch (Exception ex)
                  {
                        throw new InvalidOperationException(ex.Message);
                  }

            }
      }
}