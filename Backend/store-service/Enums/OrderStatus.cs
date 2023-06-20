using AutoMapper;
using App.Models.OrderModel;
using App.Dtos;

namespace App.Enums
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
public class OrderValueResolver : IValueResolver<Order, OrderDto, string>
{
      public string Resolve(Order source, OrderDto destination, string destMember, ResolutionContext context)
      {
            return source.Status.ToString();
      }
}
