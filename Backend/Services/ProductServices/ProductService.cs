using AutoMapper;
using App.Dtos;
using App.Models.ProductModel;
using App.Repositories;
using Microsoft.EntityFrameworkCore;
using App.Models;
using App.Areas.Products.Models;

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

            public async Task<IEnumerable<ProductDto>> GetAllAsync(ProductQs param)
            {
                  var productList = new List<ProductDto>();
                  var products = _unitOfWork.ProductRepository
                                                      .All()
                                                      .Include(p => p.Icon)
                                                      .Include(p => p.ProductCategories)
                                                      .AsQueryable();
                  if (param.CategoryIds?.Length > 0)
                  {
                        products = products.Where(p => p.ProductCategories.Select(pc => pc.CategoryId).Any(x => param.CategoryIds.Contains(x)));
                  }
                  if (param.IConId != null)
                  {
                        products = products.Where(p => param.IConId.Contains(p.IconId));
                  }
                  if (param.Name != null && param.Name != "")
                  {
                        products = products.Where(p => p.Name.Contains(param.Name));
                  }
                  if (param.MaxPrice != null)
                  {
                        products = products.Where(p => p.SalePrice < param.MaxPrice);
                  }
                  if (param.MinPrice != null)
                  {
                        products = products.Where(p => p.SalePrice > param.MinPrice);
                  }
                  if (param.OrderBy != null && param.OrderBy != "")
                  {
                        string[] orderByParts = param.OrderBy.Split(' ');
                        if (orderByParts.Length == 2)
                        {
                              string fieldName = orderByParts[0];
                              string sortOrder = orderByParts[1];
                              if (sortOrder.ToLower() == "asc")
                              {
                                    products = ApplyOrderByAscending(products, fieldName);
                              }
                              else if (sortOrder.ToLower() == "desc")
                              {
                                    products = ApplyOrderByDescending(products, fieldName);
                              }
                        }
                  }
                  else
                  {

                        products = products.OrderBy(p => p.SalePrice);
                  }

                  if (param.PageSize > 0)
                  {
                        var skip = param.PageSize * (param.CurrentPage - 1);
                        products = products.Skip(skip).Take(param.PageSize);
                  }

                  productList = _mapper.Map<List<Product>, List<ProductDto>>(await products.ToListAsync());
                  return productList;
            }
            public List<ProductDto> GetAllAsync()
            {
                  var productList = new List<ProductDto>();
                  List<Product> products = _unitOfWork.ProductRepository
                                                      .All()
                                                      .Include(p => p.ProductCategories)
                                                      .ToList();
                  productList = _mapper.Map<List<Product>, List<ProductDto>>(products);
                  return productList;
            }


            public ProductDto? GetById(int id)
            {
                  return _mapper.Map<Product, ProductDto>(_unitOfWork.ProductRepository.Get(id));
            }

            public async Task UpdateAsync(EditProductViewModel model)
            {
                  try
                  {
                        var product = _unitOfWork.ProductRepository.All().Include(p => p.ProductCategories).FirstOrDefault(p => p.Id == model.Id);
                        if (product == null) throw new Exception("Product is not available");
                        var removes = product.ProductCategories.Where(pc => !model.CategoryIDs.Contains(pc.CategoryId)).ToList();
                        foreach (var remove in removes)
                        {
                              _unitOfWork.ProductCategoryRepository.Delete(remove);
                        }
                        var newpcids = model.CategoryIDs.Where(x => !product.ProductCategories.Select(pc => pc.CategoryId).Contains(x)).ToArray();
                        foreach (var newpcid in newpcids)
                        {
                              var newPC = new ProductCategory()
                              {
                                    ProductId = product.Id,
                                    CategoryId = newpcid
                              };
                              _unitOfWork.ProductCategoryRepository.Add(newPC);
                        }
                        product.UpdateAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

                        _unitOfWork.ProductRepository.Update(product);
                        await _unitOfWork.CompleteAsync();
                  }
                  catch (Exception ex)
                  {
                        throw new InvalidOperationException(ex.Message);
                  }

            }
            private IQueryable<Product> ApplyOrderByAscending(IQueryable<Product> query, string fieldName)
            {
                  switch (fieldName.ToLower())
                  {
                        case "price":
                              return query.OrderBy(p => p.Price);
                        case "name":
                              return query.OrderBy(p => p.Name);
                        // Các trường khác
                        default:
                              return query;
                  }
            }
            private IQueryable<Product> ApplyOrderByDescending(IQueryable<Product> query, string fieldName)
            {
                  switch (fieldName.ToLower())
                  {
                        case "price":
                              return query.OrderByDescending(p => p.Price);
                        case "name":
                              return query.OrderByDescending(p => p.Name);
                        // Các trường khác
                        default:
                              return query;
                  }
            }
      }


}