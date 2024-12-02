using Infrastructure;
using MediatR;
using Models;
namespace Application.Queries
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
    {
        private readonly IFakeDatabase _database;

        public GetAuthorByIdQueryHandler(IFakeDatabase database)
        {
            _database = database;
        }

        public Task<Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            var author = _database.Authors.FirstOrDefault(a => a.Id == request.Id);
            return Task.FromResult(author);
        }
    }
}
