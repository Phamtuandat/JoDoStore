using AutoMapper;
using Backend.Models.Products;
using Backend.Services.AuthorService;
using Backend.Services.Communication;
using MediatR;

namespace Backend.Command
{
    public class CreateAuthorCommand : IRequest<AuthorRes>
    {
        public SaveAuthorResourceModel SaveAuthorResourceModel { get; set; }

        public class CreateAthorCommandHandler : IRequestHandler<CreateAuthorCommand, AuthorRes>
        {
            private readonly IMapper _mapper;
            private readonly IAuthorService _authorService;

            public CreateAthorCommandHandler(IAuthorService authorService, IMapper  mapper)
            {
                _mapper = mapper;
                _authorService = authorService;
            }
            public async Task<AuthorRes> Handle(CreateAuthorCommand req, CancellationToken token)
            {
                var author = _mapper.Map<SaveAuthorResourceModel, Author>(req.SaveAuthorResourceModel);
                var result = await _authorService.SaveAsync(author);
                return result;
            }
        }
        
    }

}
