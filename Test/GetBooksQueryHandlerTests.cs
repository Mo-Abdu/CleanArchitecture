using Application.Queries;
using Infrastructure;
using Models;
using Moq;
namespace Test
{
    [TestFixture]
    public class GetBooksQueryHandlerTests
    {
        private Mock<IFakeDatabase> _mockDatabase;
        private GetBooksQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new Mock<IFakeDatabase>();
            _mockDatabase.Setup(db => db.Books).Returns(new List<Book>
            {
                new Book(1, "Book One", "Title One", "Description One", new Author(1, "Jane Doe")),
                new Book(2, "Book Two", "Title Two", "Description Two", new Author(2, "John Smith"))
            });
            _handler = new GetBooksQueryHandler(_mockDatabase.Object);
        }

        [Test]
        public async Task HandleShouldReturnAllBooks()
        {
            // Arrange
            var query = new GetBooksQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.Count, Is.EqualTo(2), "Result count should match the number of books in the database");
            Assert.That(result[0].Name, Is.EqualTo("Book One"), "First book's name should match");
            Assert.That(result[1].Name, Is.EqualTo("Book Two"), "Second book's name should match");
        }
        [Test]
        public async Task Handle_ShouldReturnAllBooks_WhenBooksExist()
        {
            // Arrange
            var books = new List<Book>
    {
        new Book(1, "Name1", "Title1", "Description1", new Author(1, "Author1")),
        new Book(2, "Name2", "Title2", "Description2", new Author(2, "Author2"))
    };
            _mockDatabase.Setup(db => db.Books).Returns(books);

            var query = new GetBooksQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2), "Result should contain all books in the database");
            Assert.That(result, Is.EqualTo(books), "Returned books should match the database");
        }
        
    }
}
