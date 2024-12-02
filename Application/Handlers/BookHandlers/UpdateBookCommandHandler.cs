using Application.Commands.BookCommands;
using Infrastructure;
using MediatR;
using Models;
namespace Application.Handlers.BookHandlers
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Book>
    {
        private readonly IFakeDatabase _database;

        public UpdateBookCommandHandler(IFakeDatabase database)
        {
            _database = database;
        }

        public Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = _database.Books.FirstOrDefault(b => b.Id == request.Id);
            if (book == null)
                throw new KeyNotFoundException("Book not found.");

            var author = _database.Authors.FirstOrDefault(a => a.Id == request.AuthorId);
            if (author == null)
                throw new KeyNotFoundException("Author not found.");

            book.Name = request.Name;
            book.Title = request.Title;
            book.Description = request.Description;
            book.Author = author;

            return Task.FromResult(book);
        }
    }    
}
