using gearshop_dotnetapp.Services.ProductServices;
using MediatR;

namespace gearshop_dotnetapp.Queries
{
    public class GetAllCategoryQuery : IRequest<IEnumerable<CategoryResource>>
    {
        public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, IEnumerable<CategoryResource>>
        {
            private readonly ICategoryService _categoryService;
            public GetAllCategoryQueryHandler(ICategoryService categoryService)
            {
                _categoryService = categoryService;
            }

            public  async Task<IEnumerable<CategoryResource>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
            {
                return await  _categoryService.GetAll();
            }
        }
    }
}
