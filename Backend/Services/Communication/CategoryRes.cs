using Backend.Models.Products;
using Backend.Resources;

namespace Backend.Services.Communication
{
    public class CategoryRes : BaseResponse
    {
        public CategoryResource CategoryResource { get; private set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public CategoryRes(bool success, string message, Category category) : base(success, message)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            if(category != null)
            {
                CategoryResource = new CategoryResource()
                {
                    Name = category.Name,
                    Id = category.Id,
                };
            }

        }
        public CategoryRes(Category category) : this(true, string.Empty, category)
        { }
        public CategoryRes(string message) : this(false, message, null) { }
        public CategoryRes(bool success) : this(success, string.Empty, null) { }
    }
}
