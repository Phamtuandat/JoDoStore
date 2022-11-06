using AutoMapper;
using Backend.Models.Products;
using Backend.Resources;
using Backend.Services.Brand;
using MediatR;

namespace Backend.Queries
{
    public class GetAllBrandQuery : IRequest<IEnumerable<BrandResource>>
    {
        public class GetAllAuthorQueryHandler : IRequestHandler<GetAllBrandQuery, IEnumerable<BrandResource>>
        {
            private readonly IBrandService _authorService;
            private readonly IMapper _mapper;

            public GetAllAuthorQueryHandler(IBrandService authorService, IMapper mapper)
            {
                _authorService = authorService;
                _mapper = mapper;
            }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            public async Task<IEnumerable<BrandResource>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
            {
                var authors =   _authorService.GetAll().ToList();
               var result = _mapper.Map<IEnumerable<BrandModel>, IEnumerable<BrandResource>>(authors);
                return result;
            }

           
        }
    }
}
