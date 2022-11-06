using Backend.Resources;
using Backend.Services.Product;
using MediatR;

namespace Backend.Command
{
    public class CreateProductCommand : IRequest<ProductRes>
    {
        public SaveProductResource SaveBookResource { get; set; } =new SaveProductResource();
        public class CreateNewBookCommandHandler : IRequestHandler<CreateProductCommand, ProductRes>
        {
            private readonly IProductService _productService;
            public CreateNewBookCommandHandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<ProductRes> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                var result = await _productService.CreateAsync(request.SaveBookResource);
                return result;
            }
        }
    }
}
