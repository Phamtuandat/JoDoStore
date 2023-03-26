using AutoMapper;
using gearshop_dotnetapp.Models.OrderModel;
using gearshop_dotnetapp.Resources;

namespace gearshop_dotnetapp.Enums
{
    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}
public class OrderValueResolver : IValueResolver<Order, OrderResource, string>
{
    public string Resolve(Order source, OrderResource destination, string destMember, ResolutionContext context)
    {
        return source.Status.ToString();
    }
}
