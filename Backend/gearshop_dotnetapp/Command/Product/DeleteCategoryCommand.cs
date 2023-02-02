using gearshop_dotnetapp.Services.ProductServices;
using MediatR;

namespace gearshop_dotnetapp.Command.Product
{
    public class DeleteCategoryCommand : IRequest<CategoryRes>
    {
        public int Id { get; set; } 
        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, CategoryRes>
        {
            private readonly ICategoryService _categoryService;
            public DeleteCategoryCommandHandler(ICategoryService categoryService)
            {
                _categoryService = categoryService;
            }

            public async Task<CategoryRes> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                return await _categoryService.DeleteAsync(request.Id);
            }
        }
    }
}
