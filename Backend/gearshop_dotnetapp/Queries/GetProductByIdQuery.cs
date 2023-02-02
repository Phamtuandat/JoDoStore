using gearshop_dotnetapp.Resources;
using gearshop_dotnetapp.Services.Communications;
using gearshop_dotnetapp.Services.ProductServices;
using MediatR;

namespace gearshop_dotnetapp.Queries
{
    public class GetProductByIdQuery : IRequest<ProductRes>
    {
        public int Id { get; set; }

        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductRes>
        {
            private readonly IProductService _productService;
            public GetProductByIdQueryHandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<ProductRes> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
            {
                var result =  _productService.GetById(request.Id);
                return result;
            }
        }
    }
}