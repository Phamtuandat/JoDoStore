using gearshop_dotnetapp.Enums;
using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Models.OrderModel;
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp.Services.OrderServices
{
    public interface IOrderService
    {
        OrderResource GetOrderById(int orderId);
        IEnumerable<OrderResource> GetAllOrders();
        Task<OrderResource> CreateOrderAsync(SaveOrderResource model, User user);
        Task<OrderResource> UpdateOrderAsync(SaveOrderResource model, int id);
        Task DeleteOrderAsync(int orderId);
        IEnumerable<OrderResource> GetOrdersByUser(User user);
        Task<OrderResource> UpdateStatusAsync(int id, OrderStatus status);
    }
}
