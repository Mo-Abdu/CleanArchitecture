using Application.Commands.BookCommands;
using Application.Handlers.BookHandlers;
using Infrastructure;
using Models;
using Moq;
namespace Test
{
    [TestFixture]
    public class UpdateBookCommandHandlerTests
    {
        private Mock<IFakeDatabase> _mockDatabase;
        private UpdateBookCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new Mock<IFakeDatabase>();
            _mockDatabase.Setup(db => db.Authors).Returns(new List<Author>
            {
                new Author(1, "Jane Doe")
            });
            _mockDatabase.Setup(db => db.Books).Returns(new List<Book>
            {
                new Book(1, "Old Name", "Old Title", "Old Description", new Author(1, "Jane Doe"))
            });
            _handler = new UpdateBookCommandHandler(_mockDatabase.Object);
        }

        [Test]
        public async Task HandleShouldUpdateBookInDatabase()
        {
            // Arrange
            var command = new UpdateBookCommand(1, "New Name", "New Title", "New Description", 1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.Name, Is.EqualTo("New Name"), "Book name should be updated");
            Assert.That(result.Title, Is.EqualTo("New Title"), "Book title should be updated");
            Assert.That(result.Description, Is.EqualTo("New Description"), "Book description should be updated");
            Assert.That(_mockDatabase.Object.Books.First(b => b.Id == 1).Name, Is.EqualTo("New Name"), "Database book should be updated");
        }
        [Test]
        public async Task Handle_ShouldUpdateBook_WhenValidDataProvided()
        {
            // Arrange
            var book = new Book(1, "Old Name", "Old Title", "Old Description", new Author(1, "Author"));
            _mockDatabase.Setup(db => db.Books).Returns(new List<Book> { book });
            var command = new UpdateBookCommand(1, "New Name", "New Title", "New Description", 1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Name, Is.EqualTo("New Name"), "Book name should be updated");
            Assert.That(result.Title, Is.EqualTo("New Title"), "Book title should be updated");
            Assert.That(result.Description, Is.EqualTo("New Description"), "Book description should be updated");
        }
        [Test]
        public void Handle_ShouldThrowKeyNotFoundException_WhenBookNotFound()
        {
            // Arrange
            var command = new UpdateBookCommand(99, "New Name", "New Title", "New Description", 1); // Non-existing book

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _handler.Handle(command, CancellationToken.None),
                "Expected KeyNotFoundException for missing book"
            );
        }
        [Test]
        public void Handle_ShouldThrowKeyNotFoundException_WhenAuthorNotFound()
        {
            // Arrange
            var book = new Book(1, "Old Name", "Old Title", "Old Description", new Author(1, "Author"));
            _mockDatabase.Setup(db => db.Books).Returns(new List<Book> { book });
            var command = new UpdateBookCommand(1, "New Name", "New Title", "New Description", 99); // Non-existing author

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _handler.Handle(command, CancellationToken.None),
                "Expected KeyNotFoundException for missing author"
            );
        }

    }
}
