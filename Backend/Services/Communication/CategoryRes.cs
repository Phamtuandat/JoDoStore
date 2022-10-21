using Backend.Models.Products;
using Backend.Resources;

namespace Backend.Services.Communication
{
    public class CategoryRes : BaseResponse
    {
        public CategoryResource CategoryResource { get; private set; }
        public CategoryRes(bool success, string message, Category category) : base(success, message)
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
