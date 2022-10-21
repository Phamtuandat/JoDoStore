using Backend.Services.Communication;
using Backend.Services.Product;
using MediatR;

namespace Backend
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
                var result = await _categoryService.DeleteAsync(request.Id);
                return result;  
            }
        }
    }
}