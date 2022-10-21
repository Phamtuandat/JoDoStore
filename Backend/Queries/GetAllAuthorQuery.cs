using AutoMapper;
using Backend.Models.Products;
using Backend.Resources;
using Backend.Services.AuthorService;
using MediatR;

namespace Backend.Queries
{
    public class GetAllAuthorQuery : IRequest<IEnumerable<AuthorResource>>
    {
        public class GetAllAuthorQueryHandler : IRequestHandler<GetAllAuthorQuery, IEnumerable<AuthorResource>>
        {
            private readonly IAuthorService _authorService;
            private readonly IMapper _mapper;

            public GetAllAuthorQueryHandler(IAuthorService authorService, IMapper mapper)
            {
                _authorService = authorService;
                _mapper = mapper;
            }
            public async Task<IEnumerable<AuthorResource>> Handle(GetAllAuthorQuery request, CancellationToken cancellationToken)
            {
                var authors =   _authorService.GetAll().ToList();
               return  _mapper.Map<IEnumerable<Author>, IEnumerable<AuthorResource>>(authors);
            }
        }
    }
}
