using Backend.Resources;
using Backend.Services.Product;
using MediatR;

namespace Backend.Command
{
    public class CreateNewBookCommand : IRequest<BookRes>
    {
        public SaveBookResource SaveBookResource { get; set; } 
        public class CreateNewBookCommandHandler : IRequestHandler<CreateNewBookCommand, BookRes>
        {
            private readonly IBookService _bookService;
            public CreateNewBookCommandHandler(IBookService bookService)
            {
                _bookService = bookService;
            }

            public async Task<BookRes> Handle(CreateNewBookCommand request, CancellationToken cancellationToken)
            {
                var result = await _bookService.CreateAsync(request.SaveBookResource);
                return result;
            }
        }
    }
}
