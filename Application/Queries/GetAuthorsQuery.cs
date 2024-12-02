using MediatR;
using Models;
namespace Application.Queries
{
    public class GetAuthorsQuery : IRequest<List<Author>>
    {
  
    }
}
