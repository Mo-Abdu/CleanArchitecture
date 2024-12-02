using MediatR;
using Models;
namespace Application.Queries
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public int Id { get; }

        public GetBookByIdQuery(int id)
        {
            Id = id;
        }
    }
}
