using MediatR;
using Models;
namespace Application.Commands.BookCommands
{
    public class AddBookCommand : IRequest<Book>
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }

        public AddBookCommand(string name, string title, string description, int authorId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            AuthorId = authorId;
        }
    }
}
