using Backend.Resources;
using Backend.Services.Product;
using MediatR;

namespace Backend
{
    internal class UpdateBookCommand : IRequest<BookRes>
    {
        public SaveBookResource SaveBookResource { get; set; }
        public int Id { get; set; }

        public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookRes>
        {
            private readonly IBookService _bookService;
            public UpdateBookCommandHandler(IBookService bookService)
            {
                _bookService = bookService;
            }

            public async Task<BookRes> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
            {
                var result =  await _bookService.UpdateAsync(request.SaveBookResource, request.Id);
                return result;
            }

        }
    }
}