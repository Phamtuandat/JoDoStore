using AutoMapper;
using gearshop_dotnetapp.Enums;
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
        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderResource> CreateOrderAsync(SaveOrderResource model, User user)
        {
            try
            {
                decimal subTotal = 0;
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
                        UnitPrice = product.SalePrice,

                    };
                    subTotal += orderItem.TotalPrice;
                    orderItems.Add(orderItem);
                    _unitOfWork.OrderItemRepository.Add(orderItem);
                }
                var address = _unitOfWork.AddressRepository.Get(model.AddressId);
                if (address == null)
                {
                    throw new Exception($"address with id {model.AddressId} not found.");
                }
                var order = new Order()
                {
                    OrderDate = DateTime.UtcNow,
                    AddressBook = address,
                    OrderItems = orderItems,
                    User = user,
                    ShippingCash = model.ShippingCash,
                    SubtotalPrice = subTotal,
                    TotalPrice = subTotal + model.ShippingCash
                };
                var result = _unitOfWork.OrderRepository.Add(order);
                await _unitOfWork.CompleteAsync();
                return _mapper.Map<OrderResource>(result);
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

        public IQueryable<OrderResource> GetAllOrders()
        {
            var result = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(_unitOfWork.OrderRepository.All().ToList());
            return result.AsQueryable();
        }

        public OrderResource GetOrderById(int orderId)
        {
            var order = _unitOfWork.OrderRepository.All().FirstOrDefault();
            if (order == null)
            {
                throw new OrderNotFoundException($"could not find orderId {orderId} please try again!");
            }

            return _mapper.Map<OrderResource>(order);
        }

        public IEnumerable<OrderResource> GetOrdersByUser(User user)
        {
            var list = _unitOfWork.OrderRepository.All().Where(o => o.User.Id == user.Id).ToList();
            var result = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(list);
            return result;
        }

        public async Task<OrderResource> UpdateOrderAsync(SaveOrderResource model, int id)
        {
            try
            {
                var order = _unitOfWork.OrderRepository.Get(id);

                if (order == null)
                {
                    throw new OrderNotFoundException($"could not find orderId {id} please try again!");
                }
                var address = _unitOfWork.AddressRepository.Get(id);
                if (address == null)
                {
                    throw new Exception(" address could not found");
                }
                order.OrderDate = DateTime.Now;
                order.AddressBook = address;
                var orderEdited = _unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.CompleteAsync();
                return _mapper.Map<OrderResource>(orderEdited);

            }
            catch (Exception ex)
            {
                throw new OrderProcessingException(ex.Message);
            }
        }

        public async Task<OrderResource> UpdateStatusAsync(int id, OrderStatus status)
        {
            var order = _unitOfWork.OrderRepository.Get(id);
            if (order == null) throw new OrderNotFoundException("Notfound orderId!");
            order.Status = status;
            var result = _unitOfWork.OrderRepository.Update(order);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<Order, OrderResource>(result);
        }
    }
}
