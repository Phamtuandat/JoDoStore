using gearshop_dotnetapp.Models.OrderModel;

namespace gearshop_dotnetapp.Services.OrderServices
{
    public interface IOrderItemService
    {
        IEnumerable<OrderItem> GetOrderItemsByOrderId(int orderId);
        Task AddOrderItemAsync(OrderItem orderItem);
        Task UpdateOrderItemAsync(OrderItem orderItem);
        Task DeleteOrderItemAsync(int orderItemId);
    }
}
