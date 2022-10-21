using AutoMapper;
using Backend.Models.Products;
using Backend.Services.AuthorService;
using Backend.Services.Communication;
using MediatR;

namespace Backend.Command
{
    public class UpdateAuthorCommand : IRequest<AuthorRes>
    {
        public int Id { get; set; }
        public SaveAuthorResourceModel SaveAuthorResourceModel { get; set; }

        public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, AuthorRes>
        {
            private readonly IAuthorService _authorService;
            private readonly IMapper _mapper;

            public UpdateAuthorCommandHandler(IAuthorService authorService, IMapper mapper)
            {
                _authorService = authorService;
                _mapper = mapper;
            }

            public async Task<AuthorRes> Handle(UpdateAuthorCommand req, CancellationToken token)
            {
                var author = _mapper.Map<SaveAuthorResourceModel, Author>(req.SaveAuthorResourceModel);
                var result = await _authorService.UpdateAsync(author, req.Id);
                return result;
            }

           
        }

    }
}
