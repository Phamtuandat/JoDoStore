using gearshop_dotnetapp.Exceptions;
using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Models.OrderModel;
using gearshop_dotnetapp.Repositories;
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderService( IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
        }

        public async Task<Order> CreateOrderAsync(SaveOrderResource model, User user)
        {
            try
            {
                var orderItems = new List<OrderItem>();
                foreach (var item in model.OrderItems)
                {
                    var product = _unitOfWork.ProductRepository.Get(item.ProductId);
                    if (product == null)
                    {
                        throw new ProductNotFoundException($"Product with id {item.ProductId} not found.");
                    }
                    var orderItem = new OrderItem
                    {
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        UnitPrice = product.SalePrice
                    };
                    orderItems.Add(orderItem);
                    _unitOfWork.OrderItemRepository.Add(orderItem);
                }
                var address = _unitOfWork.AdressRepository.Get(model.AddressId);
                if(address == null)
                {
                    throw new Exception($"address with id {model.AddressId} not found.");
                }
                var order = new Order()
                {
                    OrderDate = DateTime.UtcNow,
                    Adress = address,
                    OrderItems = orderItems,
                    User = user
                };
                var result = _unitOfWork.OrderRepository.Add(order);
                await _unitOfWork.CompleteAsync();
                return result;
            }
            catch (Exception ex)
            {

                throw new OrderProcessingException($"Something went wrong, \n Ex: {ex.Message}");
            }
            
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.Get(orderId);
                if (order == null)
                {
                    throw new OrderNotFoundException($"could not find orderId {orderId} please try again!");
                }
                _unitOfWork.OrderRepository.Delete(order);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {

                throw new OrderProcessingException($"Something went wrong, \n Ex: {ex.Message}");
            }
           
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _unitOfWork.OrderRepository.All().ToList();
        }

        public Order GetOrderById(int orderId)
        {
            var order = _unitOfWork.OrderRepository.All().FirstOrDefault();
            if(order == null)
            {
                throw new OrderNotFoundException($"could not find orderId {orderId} please try again!");
            }
            return order;
        }

        public async Task<Order> UpdateOrderAsync(SaveOrderResource model, int id)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.Get(id);
            
                if (order == null)
                {
                    throw new OrderNotFoundException($"could not find orderId {id} please try again!");
                }
                var address = _unitOfWork.AdressRepository.Get(id);
                if(address == null)
                {
                    throw new Exception(" address could not found");
                }
                order.OrderDate = DateTime.Now;
                order.Adress = address;
                var orderEdited = _unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.CompleteAsync();
                return orderEdited;

            }
            catch (Exception ex)
            {
                throw new OrderProcessingException(ex.Message);
            }
        }
    }
}
