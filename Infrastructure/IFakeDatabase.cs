using Models;
namespace Infrastructure
{
    public interface IFakeDatabase
    {
        List<Book> Books { get; set; }
        List<Author> Authors { get; set; }
    }
}
