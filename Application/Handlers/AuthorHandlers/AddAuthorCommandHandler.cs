using Application.Commands.AuthorCommands;
using Infrastructure;
using MediatR;
using Models;
namespace Application.Handlers.AuthorHandlers
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Author>
    {
        private readonly IFakeDatabase _database;

        public AddAuthorCommandHandler(IFakeDatabase database)
        {
            _database = database;
        }

        public Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Author(_database.Authors.Count + 1, request.Name);
            _database.Authors.Add(author);
            return Task.FromResult(author);
        }
    }
}
