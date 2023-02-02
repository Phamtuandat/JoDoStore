using gearshop_dotnetapp.Resources;
using gearshop_dotnetapp.Services.ProductServices;
using MediatR;

namespace gearshop_dotnetapp
{
    public class GetAllProductQuery : IRequest<IEnumerable<ProductResource>>
    {
        public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<ProductResource>>
        {
            private readonly IProductService _productService;
            public GetAllProductQueryHandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<IEnumerable<ProductResource>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
            {
                var result = await _productService.GetAll();
                return result;
            }
        }
    }
}