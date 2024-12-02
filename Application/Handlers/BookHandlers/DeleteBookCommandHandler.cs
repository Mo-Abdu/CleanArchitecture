using Application.Commands.BookCommands;
using Infrastructure;
using MediatR;
namespace Application.Handlers.BookHandlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IFakeDatabase _database;

        public DeleteBookCommandHandler(IFakeDatabase database)
        {
            _database = database;
        }
        public Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = _database.Books.FirstOrDefault(b => b.Id == request.Id);

            if (book == null)
                throw new KeyNotFoundException("Book not found.");

            _database.Books.Remove(book);
            return Task.FromResult(true);
        }
    }
}
