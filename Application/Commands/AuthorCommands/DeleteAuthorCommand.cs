using MediatR;
namespace Application.Commands.AuthorCommands
{
    public class DeleteAuthorCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteAuthorCommand(int id)
        {
            Id = id;
        }
    }
}
