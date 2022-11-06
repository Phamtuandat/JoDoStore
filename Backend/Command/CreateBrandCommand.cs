using AutoMapper;
using Backend.Models.Products;
using Backend.Services.Brand;
using Backend.Services.Communication;
using MediatR;

namespace Backend.Command
{
    public class CreateAuthorCommand : IRequest<BrandRes>
    {
        public SaveAuthorResourceModel SaveAuthorResourceModel { get; set; } = new();

        public class CreateAthorCommandHandler : IRequestHandler<CreateAuthorCommand, BrandRes>
        {
            private readonly IMapper _mapper;
            private readonly IBrandService _brandService;

            public CreateAthorCommandHandler(IBrandService brandService, IMapper  mapper)
            {
                _mapper = mapper;
                _brandService = brandService;
            }
            public async Task<BrandRes> Handle(CreateAuthorCommand req, CancellationToken token)
            {
                var brand = _mapper.Map<SaveAuthorResourceModel, BrandModel>(req.SaveAuthorResourceModel);
                var result = await _brandService.SaveAsync(brand);
                return result;
            }
        }
        
    }

}
