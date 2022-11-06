using AutoMapper;
using Backend.Models.Products;
using Backend.Services.Brand;
using Backend.Services.Communication;
using MediatR;

namespace Backend.Command
{
    public class UpdateBrandCommand : IRequest<BrandRes>
    {
        public int Id { get; set; }
        public SaveAuthorResourceModel SaveBrandResourceModel { get; set; } = new();

        public class UpdateAuthorCommandHandler : IRequestHandler<UpdateBrandCommand, BrandRes>
        {
            private readonly IBrandService _brandService;
            private readonly IMapper _mapper;

            public UpdateAuthorCommandHandler(IBrandService brandService, IMapper mapper)
            {
                _brandService = brandService;
                _mapper = mapper;
            }

            public async Task<BrandRes> Handle(UpdateBrandCommand req, CancellationToken token)
            {
                var author = _mapper.Map<SaveAuthorResourceModel, BrandModel>(req.SaveBrandResourceModel);
                var result = await _brandService.UpdateAsync(author, req.Id);
                return result;
            }

           
        }

    }
}
