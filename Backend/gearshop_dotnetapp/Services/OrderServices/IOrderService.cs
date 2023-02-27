using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Models.OrderModel;
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp.Services.OrderServices
{
    public interface IOrderService
    {
        Order GetOrderById(int orderId);
        IEnumerable<Order> GetAllOrders();
        Task<Order> CreateOrderAsync(SaveOrderResource model, User user);
        Task<Order> UpdateOrderAsync(SaveOrderResource model, int id);
        Task DeleteOrderAsync(int orderId);
    }
}
