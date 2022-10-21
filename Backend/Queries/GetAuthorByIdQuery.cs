using Backend.Resources;
using Backend.Services.AuthorService;
using Backend.Services.Communication;
using MediatR;

namespace Backend
{
    internal class GetAuthorByIdQuery : IRequest<AuthorRes>
    {
        public int Id { get; set; }

        public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, AuthorRes>
        {
            private readonly IAuthorService _authorService;

            public GetAuthorByIdQueryHandler(IAuthorService authorService)
            {
                _authorService = authorService;
            }
            public async Task<AuthorRes> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
            {
                var result = _authorService.GetById(request.Id);
                return result;
            }
        }
    }
}