using App.Enums;
using App.Models.OrderModel;

namespace App.Dtos
{
      public class OrderDto
      {
            public int Id { get; set; }
            public decimal ShippingCash { get; set; }
            public decimal TotalPrice { get; set; }
            public decimal SubtotalPrice { get; set; }
            public DateTime OrderDate { get; set; }
            public string Status { get; set; } = string.Empty;
            public string UserName { get; set; } = string.Empty;
      }
}
