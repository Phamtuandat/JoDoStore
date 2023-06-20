using App.Enums;
using App.Models.Identity;
using App.Dtos;
using App.Models.OrderModel;

namespace App.Services.OrderServices
{
      public interface IOrderService
      {
            OrderDto GetOrderById(int orderId);
            IQueryable<OrderDto> GetAllOrders();
            Task<OrderDto> CreateOrderAsync(Order model, User user);
            Task<OrderDto> UpdateOrderAsync(Order model, int id);
            Task DeleteOrderAsync(int orderId);
            IEnumerable<OrderDto> GetOrdersByUser(User user);
            Task<OrderDto> UpdateStatusAsync(int id, OrderStatus status);
      }
}
