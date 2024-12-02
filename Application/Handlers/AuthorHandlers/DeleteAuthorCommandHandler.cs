using Application.Commands.AuthorCommands;
using Infrastructure;
using MediatR;
namespace Application.Handlers.AuthorHandlers
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>
    {
        private readonly IFakeDatabase _database;

        public DeleteAuthorCommandHandler(IFakeDatabase database)
        {
            _database = database;
        }

        public Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = _database.Authors.FirstOrDefault(a => a.Id == request.Id);

            if (author == null)
                throw new KeyNotFoundException("Author not found.");

            _database.Authors.Remove(author);
            return Task.FromResult(true);
        }
    }
}
