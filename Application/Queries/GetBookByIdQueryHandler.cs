using Infrastructure;
using MediatR;
using Models;
namespace Application.Queries
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IFakeDatabase _database;

        public GetBookByIdQueryHandler(IFakeDatabase database)
        {
            _database = database;
        }

        public Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = _database.Books.FirstOrDefault(b => b.Id == request.Id);
            return Task.FromResult(book);
        }
    }
}
