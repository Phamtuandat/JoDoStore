using Backend.Services.Product;
using MediatR;

namespace Backend
{
    public class DeleteBookComand : IRequest<BookRes>
    {
        public int Id { get; set; }
        public class DeleteBookCommandHandler : IRequestHandler<DeleteBookComand, BookRes>
        {
            private readonly IBookService _bookService;
            public DeleteBookCommandHandler(IBookService bookService)
            {
                _bookService = bookService;
            }

            public async Task<BookRes> Handle(DeleteBookComand request, CancellationToken cancellationToken)
            {
                var result = await _bookService.DeletAsync(request.Id);
                return result;
            }

        }
    }
}