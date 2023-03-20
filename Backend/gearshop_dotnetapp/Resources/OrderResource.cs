﻿using gearshop_dotnetapp.Enums;
using gearshop_dotnetapp.Models.OrderModel;

namespace gearshop_dotnetapp.Resources
{
    public class OrderResource
    {
        public int Id { get; set; }
        public ICollection<OrderItemResource> OrderItems { get; set; }
        public decimal ShippingCash { get; set; }
        public decimal TotalPrice { get; set;}
        public decimal SubtotalPrice { get; set;}
        public AddressResource Address { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }
}