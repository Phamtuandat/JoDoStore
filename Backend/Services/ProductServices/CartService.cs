using AutoMapper;
using App.Exceptions;
using App.Models.Identity;
using App.Models.ProductModel;
using App.Repositories;
using App.Dtos;
using App.Services.ProductServices;

namespace App
{
      public class CartService : ICartService
      {
            private readonly IUnitOfWork _uniOfWork;
            private readonly IMapper _mapper;
            public CartService(IUnitOfWork unitOfWork, IMapper mapper)
            {
                  _mapper = mapper;
                  _uniOfWork = unitOfWork;
            }
            public async Task AddItemAsync(int productId, int quantity, User user)
            {
                  try
                  {
                        var cart = _uniOfWork.CartRepository.All().FirstOrDefault(x => x.UserId == user.Id);
                        if (cart == null) throw new ProductNotFoundException("not found!");
                        var item = cart.Items.FirstOrDefault(x => x.Product?.Id == productId);
                        var product = _uniOfWork.ProductRepository.Get(productId);
                        if (product == null) throw new ProductNotFoundException("not found!");
                        if (item == null)
                        {
                              var newItem = new CartItem()
                              {
                                    Product = product,
                                    Quantity = quantity,
                              };
                              cart.Items.Add(newItem);

                        }
                        else
                        {
                              item.Quantity += quantity;
                        }
                        await _uniOfWork.CompleteAsync();
                  }
                  catch (Exception ex)
                  {

                        throw new BaseException(ex.Message);
                  }

            }

            public async Task<CartDto> GetCart(User user)
            {
                  var cart = _uniOfWork.CartRepository.All().FirstOrDefault(x => x.UserId == user.Id);
                  if (cart == null)
                  {
                        cart = new Cart()
                        {
                              Items = new List<CartItem>(),
                              User = user,
                              UserId = user.Id,
                        };
                        _uniOfWork.CartRepository.Add(cart);
                        await _uniOfWork.CompleteAsync();
                  }
                  var cartResource = _mapper.Map<Cart, CartDto>(cart);
                  return cartResource;
            }

            public async Task RemoveItemAsync(int id, User user)
            {
                  var cart = _uniOfWork.CartRepository.All().FirstOrDefault(x => x.UserId == user.Id);
                  if (cart == null) throw new ProductNotFoundException("Not Found!");
                  var item = cart.Items.FirstOrDefault(x => x.ProductId == id);
                  if (item == null) throw new ProductNotFoundException("Not Found!");
                  cart.Items.Remove(item);
                  await _uniOfWork.CompleteAsync();
            }
      }
}