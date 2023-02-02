using gearshop_dotnetapp.Services.ProductServices;
using MediatR;

namespace gearshop_dotnetapp.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryRes>
    {
        public int Id { get; set; } 
        public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryRes>
        {
            private readonly ICategoryService _categoryService;
            public GetCategoryByIdQueryHandler(ICategoryService categoryService)
            {
                _categoryService = categoryService;
            }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            public async Task<CategoryRes> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
            {
                return  _categoryService.GetById(request.Id);
            }
        }
    }
}
