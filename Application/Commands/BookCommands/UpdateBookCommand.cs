using MediatR;
using Models;
namespace Application.Commands.BookCommands
{
    public class UpdateBookCommand : IRequest<Book>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }

        public UpdateBookCommand(int id, string name, string title, string description, int authorId)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            AuthorId = authorId;
        }
    }
}
