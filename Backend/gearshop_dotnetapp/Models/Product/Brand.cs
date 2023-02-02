﻿namespace gearshop_dotnetapp.Models.Product
{
    public class Brand
    {
        public Brand()
        {
            this.Products = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string Name { get; set; }  = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Product>? Products { get; set; }
    }
}
