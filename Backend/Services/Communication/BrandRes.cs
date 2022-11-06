#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
using Backend.Models.Products;
using Backend.Resources;

namespace Backend.Services.Communication
{
    public class BrandRes : BaseResponse
    {
        public BrandResource AuthorResource { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public BrandRes(bool success, string message, Models.Products.BrandModel brand) : base(success, message)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            if(brand != null)
            {
                AuthorResource = new BrandResource()
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Description = brand.Description,
                };

            }
        }
        public BrandRes(Models.Products.BrandModel brand) : this(true, string.Empty, brand) { }
        public BrandRes(string message) : this(false, message, null) { }
        public BrandRes(bool success) : this(success, string.Empty, null) { }
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
    } 
}
