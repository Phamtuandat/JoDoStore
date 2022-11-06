using Backend.Resources;
using Backend.Services.Brand;
using Backend.Services.Communication;
using MediatR;

namespace Backend
{
    internal class GetBrandByIdQuery : IRequest<BrandRes>
    {
        public int Id { get; set; }

        public class GetAuthorByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BrandRes>
        {
            private readonly IBrandService _authorService;

            public GetAuthorByIdQueryHandler(IBrandService authorService)
            {
                _authorService = authorService;
            }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            public async Task<BrandRes> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
            {
                var result = _authorService.GetById(request.Id);
                return result;
            }
        }
    }
}