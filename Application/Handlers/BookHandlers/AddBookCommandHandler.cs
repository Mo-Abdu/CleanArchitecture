using Application.Commands.BookCommands;
using Infrastructure;
using MediatR;
using Models;
namespace Application.Handlers.BookHandlers
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Book>
    {

        private readonly IFakeDatabase _database;

        public AddBookCommandHandler(IFakeDatabase database)
        {
            _database = database;
        }

        public Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            var author = _database.Authors.FirstOrDefault(a => a.Id == request.AuthorId);
            if (author == null)
                throw new KeyNotFoundException("Author not found.");

            var book = new Book(
                id: _database.Books.Count + 1,
                name: request.Name,
                title: request.Title,
                description: request.Description,
                author: author 
            );

            _database.Books.Add(book);
            return Task.FromResult(book);
        }
    }
}
