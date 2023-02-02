using gearshop_dotnetapp.Resources;
using gearshop_dotnetapp.Services.ProductServices;
using MediatR;

namespace gearshop_dotnetapp.Command.Product
{
    public class UpdateCategoryCommand : IRequest<CategoryRes>
    {
        public SaveCategoryResource SaveCategoryResource { get; set; } = new();
        public int Id { get; set; }
        public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryRes>
        {
            private readonly ICategoryService _categoryService;
            public UpdateCategoryCommandHandler(ICategoryService categoryService)
            {
                _categoryService = categoryService;
            }
            public async Task<CategoryRes> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                return await _categoryService.UpdateCategoryAsync(request.SaveCategoryResource, request.Id);
            }
        }
    }
}
