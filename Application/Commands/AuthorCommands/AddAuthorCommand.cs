using MediatR;
using Models;

namespace Application.Commands.AuthorCommands
{
    public class AddAuthorCommand : IRequest<Author>
    {
        public string Name { get; set; }

        public AddAuthorCommand(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

    }
}
