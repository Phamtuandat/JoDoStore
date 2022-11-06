using Backend.Resources;
using Backend.Services.Product;
using MediatR;

namespace Backend
{
    internal class UpdateProductCommand : IRequest<ProductRes>
    {
        public SaveProductResource SaveBookResource { get; set; } = new SaveProductResource();
        public int Id { get; set; }

        public class UpdateBookCommandHandler : IRequestHandler<UpdateProductCommand, ProductRes>
        {
            private readonly IProductService _bookService;
            public UpdateBookCommandHandler(IProductService bookService)
            {
                _bookService = bookService;
            }

            public async Task<ProductRes> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var result =  await _bookService.UpdateAsync(request.SaveBookResource, request.Id);
                return result;
            }

        }
    }
}