﻿using App.Models.ProductModel;

namespace App
{
      public class CartItem
      {
            public int Id { get; set; }
            public int ProductId { get; set; }
            public Product Product { get; set; } = new Product();
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
      }
}