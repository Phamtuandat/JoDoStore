using AutoMapper;
using Backend.Services.Communication;
using Backend.Services.Product;
using MediatR;

namespace Backend.Queries
{
    public class GetCategoryByIdQuery : IRequest<CategoryRes>
    {
        public int Id { get;  set; }

        public class QueryCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryRes>
        {
            private readonly ICategoryService _categoryService;

            public QueryCategoryByIdHandler(ICategoryService categoryService)
            {
                _categoryService = categoryService;
            }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            public async Task<CategoryRes> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
            {
                var result = _categoryService.GetById(request.Id);
                return result;
            }

            
        }
    }
}
