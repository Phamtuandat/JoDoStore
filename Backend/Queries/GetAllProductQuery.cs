using AutoMapper;
using Backend.Models.Products;
using Backend.Resources;
using Backend.Services.Product;
using MediatR;
using System.Diagnostics;

namespace Backend.Queries
{
    public class GetAllProductQuery : IRequest<IEnumerable<ProductResource>>
    {

        public class GetAllBooksQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<ProductResource>>
        {

            private readonly IProductService _bookService;
            private readonly IMapper _mapper;
            
            public GetAllBooksQueryHandler(IProductService bookService, IMapper mapper)
            {
                _bookService = bookService;
                _mapper = mapper;
            }
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            public   async Task<IEnumerable<ProductResource>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
            {
                var bookList =  _bookService
                    .GetAll().ToList();
                var resource = _mapper.Map<List<ProductModel>, List<ProductResource>>(bookList);
               
                return resource;

                
            }

            
        }
    }
}
