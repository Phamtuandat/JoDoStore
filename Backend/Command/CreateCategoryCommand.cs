using AutoMapper;
using Backend.Models.Products;
using Backend.Resources;
using Backend.Services.Communication;
using Backend.Services.Product;
using MediatR;

namespace Backend
{
    public class CreateCategoryCommand : IRequest<CategoryRes>
    {

        public SaveCategoryResource SaveCategoryResource { get; set; } = new();

        public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryRes>
        {
            private readonly ICategoryService _categoryService;
            private readonly IMapper _mapper;
            public CreateCategoryCommandHandler(ICategoryService categoryService, IMapper mapper)
            {
                _categoryService = categoryService;
                _mapper = mapper;
            }
            public async Task<CategoryRes> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var category = _mapper.Map<SaveCategoryResource, Category>(request.SaveCategoryResource);
                var result = await _categoryService.SaveCategoryAsync(category);
                return result;
            }
        }
    }
}