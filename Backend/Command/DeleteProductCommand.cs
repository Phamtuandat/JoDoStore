using Backend.Services.Product;
using MediatR;

namespace Backend
{
    public class DeleteBookComand : IRequest<ProductRes>
    {
        public int Id { get; set; }
        public class DeleteBookCommandHandler : IRequestHandler<DeleteBookComand, ProductRes>
        {
            private readonly IProductService _productService;
            public DeleteBookCommandHandler(IProductService productSevice)
            {
                _productService = productSevice;
            }

            public async Task<ProductRes> Handle(DeleteBookComand request, CancellationToken cancellationToken)
            {
                var result = await _productService.DeletAsync(request.Id);
                return result;
            }

        }
    }
}