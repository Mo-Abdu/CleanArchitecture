
namespace Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Author Author { get; set; }

        public Book(int id, string name, string title, string description, Author author)
        {
            Id = id;
            Name = name;
            Title = title;
            Description = description;
            Author = author;
        }
    }
}
