using Backend.Services.Brand;
using Backend.Services.Communication;
using MediatR;

namespace Backend.Command
{
    public class DeleteBrandCommand : IRequest<BrandRes>
    {
        public int Id { get; set; }
        public class DeleteAuthorCommandHandler : IRequestHandler<DeleteBrandCommand, BrandRes>
        {
            private readonly IBrandService _brandService;

            public DeleteAuthorCommandHandler(IBrandService brandService)
            {
                _brandService = brandService;
            }
            public async Task<BrandRes> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
            {
                return await _brandService.DeleteAsync(request.Id);
            }
        }
    }
}
