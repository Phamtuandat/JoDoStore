using AutoMapper;
using Backend.Models.Products;
using Backend.Resources;
using Backend.Services.Communication;
using Backend.Services.Product;
using MediatR;

namespace Backend
{
    public class UpdateCategoryCommand : IRequest<CategoryRes>
    {
        public SaveCategoryResource SaveCategoryResource { get; set; }
        public int Id { get; private set; }

        public class UpdateCategoyCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryRes>
        {
            private readonly IMapper _mapper;
            private readonly ICategoryService _categoryService;

            public UpdateCategoyCommandHandler(IMapper mapper, ICategoryService categoryService)
            {
                _mapper = mapper;
                _categoryService = categoryService;
            }

            public async Task<CategoryRes> Handle( UpdateCategoryCommand res, CancellationToken token)
            {
                var category = _mapper.Map<SaveCategoryResource, Category>(res.SaveCategoryResource);
                var result =  await _categoryService.UpdateCategoryAsync(category, res.Id);
                return result;
            }

            
        }
    }
}