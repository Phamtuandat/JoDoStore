namespace gearshop_dotnetapp.Resources
{
    public class CartResource
    {
        public int Id { get; set; }
        public ICollection<CartItemResource>? Items { get; set; }
    }

    public class CartItemResource
    {
        public int Id { get; set; }
        public ProductResource? Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
      
    }

    public class CartItemReq
    {
        public int Id { get; set; }
        public int Quantity { get; set; }


    }
}
