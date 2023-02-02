using gearshop_dotnetapp.Services.Communications;
using gearshop_dotnetapp.Services.ProductServices;
using MediatR;

namespace gearshop_dotnetapp.Command.Product
{
    public class DeleteProductCommand : IRequest<ProductRes>
    {
        public int Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ProductRes>
        {
            private readonly IProductService _productService;

            public DeleteProductCommandHandler(IProductService productService)
            {
                _productService = productService;
            }
            public async Task<ProductRes> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var result = await _productService.DeleteAsync(request.Id);
                return result;
            }
        }
    }
}