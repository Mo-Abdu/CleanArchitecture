using Application.Commands.AuthorCommands;
using Infrastructure;
using MediatR;
using Models;
namespace Application.Handlers.AuthorHandlers
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Author>
    {
        private readonly IFakeDatabase _database;

        public UpdateAuthorCommandHandler(IFakeDatabase database)
        {
            _database = database;
        }

        public Task<Author> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = _database.Authors.FirstOrDefault(a => a.Id == request.Id);

            if (author == null)
                throw new KeyNotFoundException("Author not found.");

            author.Name = request.Name;
            return Task.FromResult(author);
        }
    
    }
}
