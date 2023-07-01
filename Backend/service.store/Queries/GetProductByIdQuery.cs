using App.Dtos;
using App.Services.ProductServices;
using MediatR;

namespace App.Queries
{
      public class GetProductByIdQuery : IRequest<ProductDto>
      {
            public int Id { get; set; }

            public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
            {
                  private readonly IProductService _productService;
                  public GetProductByIdQueryHandler(IProductService productService)
                  {
                        _productService = productService;
                  }

                  public Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
                  {
                        var result = _productService.GetById(request.Id);
                        return Task.FromResult(result);
                  }
            }
      }
}