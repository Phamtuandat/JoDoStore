using gearshop_dotnetapp.Resources;
using gearshop_dotnetapp.Services.ProductServices;
using MediatR;

namespace gearshop_dotnetapp.Command.Product
{
    public class CreateCategoryCommand : IRequest<CategoryRes>
    {
        public SaveCategoryResource SaveCategoryResource { get; set; } = new();
        public class CreateCategoryCommandHandle : IRequestHandler<CreateCategoryCommand, CategoryRes>
        {
            private readonly ICategoryService _categoryService;
            public CreateCategoryCommandHandle(ICategoryService categoryService)
            {
                _categoryService = categoryService;
            }

            public async Task<CategoryRes> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                return await _categoryService.SaveCategoryAsync(request.SaveCategoryResource);
            }
        }
    }
}
