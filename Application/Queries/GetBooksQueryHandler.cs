using Infrastructure;
using MediatR;
using Models;
namespace Application.Queries
{
    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<Book>>
    {
        private readonly IFakeDatabase _database;

        public GetBooksQueryHandler(IFakeDatabase database)
        {
            _database = database;
        }

        public Task<List<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            if (_database.Books == null || !_database.Books.Any())
                throw new KeyNotFoundException("No books found.");

            return Task.FromResult(_database.Books);
        }
    }
}
