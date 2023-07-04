using AutoMapper;
using App.Dtos;
using App.Services.ProductServices;
using StackExchange.Redis;
using Newtonsoft.Json;
using App.Repositories;

namespace App
{
      public class CartService : ICartService
      {
            private readonly IMapper _mapper;
            private readonly IDatabase _db;
            private readonly IUnitOfWork _unitOfWork;
            public CartService( IConnectionMultiplexer muxer, IMapper mapper, IUnitOfWork unitOfWork)
            {
                  _mapper = mapper;
                  _db = muxer.GetDatabase();
                  _unitOfWork = unitOfWork;
            }
            public async Task AddItemAsync(int productId, int quantity, string userId)
            {
                  try
                  {
                        var product = _unitOfWork.ProductRepository.Find(x => x.Id == productId)?.FirstOrDefault();
                        if(product == null) throw new ArgumentException("Product not found");
                        var key = $"Cart:{userId}";
                        var cart = await _db.StringGetAsync(key);
                        if (cart.IsNullOrEmpty){
                              var newCart = new CartDto(){
                                    Items = new List<CartItemResource>(){
                                          new CartItemResource(){
                                                ProductId = productId,
                                                Quantity = quantity,
                                                SubPrice = quantity * product.SalePrice
                                          }
                                    }
                                    
                              };
                              var cartJson = JsonConvert.SerializeObject(newCart);
                              _db.StringSet(key,cartJson);
                              return;
                        }
                        var result = JsonConvert.DeserializeObject<CartDto>(cart);
                        var item = result.Items.FirstOrDefault(x => x.ProductId == productId);
                        if (item == null) {
                              result.Items.Add(new CartItemResource() { ProductId = productId, Quantity = quantity, SubPrice = quantity * product.SalePrice });
                              await _db.StringSetAsync(key,JsonConvert.SerializeObject(result));
                              return;
                        }
                        item.Quantity = item.Quantity + quantity;
                        item.SubPrice = item.Quantity * product.SalePrice;
                        var jsonCart = JsonConvert.SerializeObject(result);
                        await _db.StringSetAsync(key, jsonCart);
                        return;
                  }
                  catch (Exception)
                  {
                        throw;
                  }
            }

            public async Task<CartDto> GetCart(string userId)
            {
                  var cart = new CartDto();
                  var key = $"Cart:{userId}";
                  var result = _db.StringGet(key);
                  if(result.IsNullOrEmpty) return new CartDto();
                  CartDto cartFromRedis = JsonConvert.DeserializeObject<CartDto>(result);
                  return cartFromRedis;
            }

            public async Task RemoveItemAsync(int productId, string userId)
            {
                  var key = $"Cart:{userId}";
                  var result =await _db.StringGetAsync(key);
                  if (result.IsNullOrEmpty) return;
                  var cart = JsonConvert.DeserializeObject<CartDto>(result);
                  var cartItem = cart.Items.FirstOrDefault(x => x.ProductId == productId);
                  if(cartItem != null) cart.Items.Remove(cartItem);
                  await _db.StringSetAsync(key,JsonConvert.SerializeObject(cart));
                  return;
            }
      }
}