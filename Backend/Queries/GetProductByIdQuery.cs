using Backend.Services.Product;
using MediatR;

namespace Backend.Queries
{
    public class GetProductByIdQuery : IRequest<ProductRes>
    {
        public int Id { get; internal set; }

        public class GetBookByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductRes>
        {
            private readonly IProductService _bookService;
            public GetBookByIdQueryHandler(IProductService bookService)
            {
                _bookService = bookService;
            }

            public async  Task<ProductRes> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                return await _bookService.GetById(request.Id);
                
            }
        }

    }
}
