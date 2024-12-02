using Models;
namespace Infrastructure
{
    public class FakeDatabase : IFakeDatabase
    {
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Author> Authors { get; set; } = new List<Author>();

        public FakeDatabase()
        {
            var author1 = new Author(1, "Anna");
            var author2 = new Author(2, "John");

            Authors.Add(author1);
            Authors.Add(author2);

            Books.Add(new Book(1, "The Comedy", "Comedy",  "Comedy", author1));
            Books.Add(new Book(2, "The Fantasy", "Fantasy",  "Fantasy", author2));

        }
    }
}
