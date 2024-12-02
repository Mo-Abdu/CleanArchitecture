
using Infrastructure;
using MediatR;
using Models;
namespace Application.Queries
{
    public class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, List<Author>>
    {
        private readonly IFakeDatabase _database;

        public GetAuthorsQueryHandler(IFakeDatabase database)
        {
            _database = database;
        }

        public Task<List<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            if (_database.Authors == null || !_database.Authors.Any())
                throw new KeyNotFoundException("No authors found.");

            return Task.FromResult(_database.Authors);
        }
    }
}
