using gearshop_dotnetapp.Services.Communications;
using gearshop_dotnetapp.Services.ProductServices;
using MediatR;

namespace gearshop_dotnetapp.Command.Product
{
    public class UpdateProducCommand : IRequest<ProductRes>
    {
        public SaveProductResource? SaveProductResource { get; set; }
        public int Id { get; set; }

        public class UpdateProducCommandHandler : IRequestHandler<UpdateProducCommand, ProductRes>
        {
            private readonly IProductService _productService;
            public UpdateProducCommandHandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<ProductRes> Handle(UpdateProducCommand request, CancellationToken cancellationToken)
            {
                if(request.SaveProductResource != null) {
                    return await _productService.UpdateAsync(request.SaveProductResource, request.Id);
                }
                return new ProductRes("product id is require!");
            }
        }
    }
}