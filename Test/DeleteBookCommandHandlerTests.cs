using Application.Commands.BookCommands;
using Application.Handlers.BookHandlers;
using Infrastructure;
using Models;
using Moq;
namespace Test
{
    [TestFixture]
    public class DeleteBookCommandHandlerTests
    {
        private Mock<IFakeDatabase> _mockDatabase;
        private DeleteBookCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new Mock<IFakeDatabase>();
            _mockDatabase.Setup(db => db.Books).Returns(new List<Book>
            {
                new Book(1, "Book Name", "Book Title", "Description", new Author(1, "Jane Doe"))
            });
            _handler = new DeleteBookCommandHandler(_mockDatabase.Object);
        }

        [Test]
        public async Task HandleShouldDeleteBookFromDatabase()
        {
            // Arrange
            var command = new DeleteBookCommand(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True, "Result should be true for successful deletion");
            Assert.That(_mockDatabase.Object.Books, Is.Empty, "Books list should be empty after deletion");
        }
        [Test]
        public async Task Handle_ShouldDeleteBook_WhenBookExists()
        {
            // Arrange
            var book = new Book(1, "Name", "Title", "Description", new Author(1, "Author"));
            _mockDatabase.Setup(db => db.Books).Returns(new List<Book> { book });
            var command = new DeleteBookCommand(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True, "Handler should return true on successful deletion");
            Assert.That(_mockDatabase.Object.Books, Does.Not.Contain(book), "Book should be removed from database");
        }
        [Test]
        public void Handle_ShouldThrowKeyNotFoundException_WhenBookNotFound()
        {
            // Arrange
            var command = new DeleteBookCommand(99); 

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _handler.Handle(command, CancellationToken.None),
                "Expected KeyNotFoundException for missing book"
            );
        }
    }
}
