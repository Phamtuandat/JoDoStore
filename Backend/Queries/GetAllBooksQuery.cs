using AutoMapper;
using Backend.Models.Products;
using Backend.Resources;
using Backend.Services.Product;
using MediatR;
using System.Diagnostics;

namespace Backend.Queries
{
    public class GetAllBooksQuery : IRequest<IEnumerable<BookResource>>
    {

        public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<BookResource>>
        {

            private readonly IBookService _bookService;
            private readonly IMapper _mapper;
            
            public GetAllBooksQueryHandler(IBookService bookService, IMapper mapper)
            {
                _bookService = bookService;
                _mapper = mapper;
            }
            public   async Task<IEnumerable<BookResource>> Handle(GetAllBooksQuery req, CancellationToken cancellationToken)
            {
                var bookList =  _bookService
                    .GetAll().ToList();
                var resource = _mapper.Map<IEnumerable<Book>, IEnumerable<BookResource>>(bookList);
                return resource;
                
            }

            
        }
    }
}
