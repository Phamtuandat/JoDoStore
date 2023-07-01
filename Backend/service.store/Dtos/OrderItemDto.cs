using App.Models.ProductModel;

namespace App.Dtos
{
      public class OrderItemDto
      {
            public int Id { get; set; }
            public decimal Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal TotalPrice { get; set; }
            public int ProductId { get; set; }
      }
}