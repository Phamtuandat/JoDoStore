using App.Models.OrderModel;

namespace App.Services.OrderServices
{
      public interface IOrderItemService
      {
            IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId);
            Task AddOrderItemAsync(OrderItem orderItem);
            Task UpdateOrderItemAsync(OrderItem orderItem);
            Task DeleteOrderItemAsync(int orderItemId);
      }
}
