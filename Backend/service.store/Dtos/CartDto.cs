namespace App.Dtos
{
      public class CartDto
      {
            public int Id { get; set; }
            public ICollection<CartItemResource>? Items { get; set; }
            
      }

      public class CartItemResource
      {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public decimal? SubPrice { get; set; }

      }

      public class CartItemReq
      {
            public int Id { get; set; }
            public int Quantity { get; set; }
      }
}
