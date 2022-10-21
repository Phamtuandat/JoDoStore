using AutoMapper;
using Backend.Models.Products;
using Backend.Resources;
using Backend.Services.Product;
using MediatR;

namespace Backend.Queries
{
    public class GetAllCategoriesQuery : IRequest<IEnumerable<CategoryResource>>
    {
        public class QueryAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryResource>>
        {
            private readonly IMapper _mapper;
            private readonly ICategoryService _categoryService;

            public QueryAllCategoriesHandler(ICategoryService categoryService, IMapper mapper)
            {
                _mapper = mapper;
                _categoryService = categoryService;
            }

            public async Task<IEnumerable<CategoryResource>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
            {
                var categories = _categoryService.GetAll().ToList();
                var result = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(categories);
                return result;
            }
        }
    }
}
