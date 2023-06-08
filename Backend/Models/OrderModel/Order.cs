using System.ComponentModel.DataAnnotations.Schema;
using App.Enums;
using App.Models.Identity;

namespace App.Models.OrderModel
{
      public class Order
      {
            public int Id { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal TotalPrice { get; set; }
            public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
            public virtual User User { get; set; } = new User();

            public int AddressId { get; set; }
            [ForeignKey("AddressId")]
            public Address Address { get; set; }
            public decimal ShippingCash { get; set; } = 0;
            public decimal SubtotalPrice { get; set; }
            public OrderStatus Status { get; set; }
      }
}
