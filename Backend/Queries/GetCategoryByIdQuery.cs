using AutoMapper;
using Backend.Models.Products;
using Backend.Resources;
using Backend.Services.Product;
using MediatR;

namespace Backend.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryResource>
    {
        public int Id { get;  set; }

        public class QueryCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryResource>
        {
            private readonly IMapper _mapper;
            private readonly ICategoryService _categoryService;

            public QueryCategoryByIdHandler(ICategoryService categoryService, IMapper mapper)
            {
                _mapper = mapper;
                _categoryService = categoryService;
            }
            
            public async Task<CategoryResource> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
            {
                var category = _categoryService.GetById(request.Id);
                return (_mapper.Map<Category, CategoryResource>(category));

            }

            
        }
    }
}
