using AutoMapper;
using Backend.Models.Products;
using Backend.Resources;
using Backend.Services.Product;
using MediatR;

namespace Backend.Queries
{
    public class GetBookByIdQuery : IRequest<BookRes>
    {
        public int Id { get; internal set; }

        public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookRes>
        {
            private readonly IBookService _bookService;
            private readonly IMapper _mapper;
            public GetBookByIdQueryHandler(IBookService bookService, IMapper mapper)
            {
                _bookService = bookService;
                _mapper = mapper;
            }

            public async  Task<BookRes> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
            {
                return await _bookService.GetById(request.Id);
                
            }
        }

    }
}
