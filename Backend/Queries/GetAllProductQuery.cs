using App.Dtos;
using App.Models.ProductModel;
using App.Services.ProductServices;
using MediatR;

namespace App
{
      public class GetAllProductQuery : IRequest<IEnumerable<ProductDto>>
      {
            public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<ProductDto>>
            {
                  private readonly IProductService _productService;
                  public GetAllProductQueryHandler(IProductService productService)
                  {
                        _productService = productService;
                  }

                  public async Task<IEnumerable<ProductDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
                  {
                        var result = _productService.GetAllAsync();
                        return result;
                  }
            }
      }
}