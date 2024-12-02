using MediatR;
using Models;
namespace Application.Commands.AuthorCommands
{
    public class UpdateAuthorCommand : IRequest<Author>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public UpdateAuthorCommand(int id, string name)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

    }
}
