using Backend.Services.Communication;

namespace gearshop_dotnetapp
{
    public class CategoryRes : BaseResponse
    {
        public CategoryResource CategoryResourceCategoryResource { get; private set; }
        public CategoryRes(bool success, string message, CategoryResource categoryResourceCategoryResource) : base(success, message)
        {
            CategoryResourceCategoryResource = categoryResourceCategoryResource;
        }

        public CategoryRes(CategoryResource category) : this(true, string.Empty, category)
        {
        }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public CategoryRes(string message) : this(false, message, null)
        {
        }
        public CategoryRes(bool success) : this(success, string.Empty, null)
        {
        }
    }
}
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.