using Backend.Services.AuthorService;
using Backend.Services.Communication;
using MediatR;

namespace Backend.Command
{
    public class DeleteAuthorCommand : IRequest<AuthorRes>
    {
        public int Id { get; set; }
        public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, AuthorRes>
        {
            private readonly IAuthorService _authorService;

            public DeleteAuthorCommandHandler(IAuthorService authorService)
            {
                _authorService = authorService;
            }
            public async Task<AuthorRes> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
            {
                return await _authorService.DeleteAsync(request.Id);
            }
        }
    }
}
