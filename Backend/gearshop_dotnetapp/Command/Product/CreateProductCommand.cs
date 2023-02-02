using gearshop_dotnetapp.Services.Communications;
using gearshop_dotnetapp.Services.ProductServices;
using MediatR;

namespace gearshop_dotnetapp.Command.Product
{
    public class CreateProductCommand : IRequest<ProductRes>
    {
        public SaveProductResource? SaveProductResource { get; set; }

        public class CreateProductCommandhandler : IRequestHandler<CreateProductCommand, ProductRes>
        {
            private readonly IProductService _productService;
            public CreateProductCommandhandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<ProductRes> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                if (request.SaveProductResource != null)
                {
                    var result = await _productService.CreateAsync(request.SaveProductResource);
                    return result;
                }
                throw new NotImplementedException("Save Product Model is required!");
            }
        }
    }
}