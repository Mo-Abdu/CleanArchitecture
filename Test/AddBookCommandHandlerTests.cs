using Application.Commands.BookCommands;
using Application.Handlers.BookHandlers;
using Infrastructure;
using Models;
using Moq;
namespace Test
{
    [TestFixture]
    public class AddBookCommandHandlerTests
    {
        private Mock<IFakeDatabase> _mockDatabase;
        private AddBookCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new Mock<IFakeDatabase>();
            _mockDatabase.Setup(db => db.Authors).Returns(new List<Author>
            {
                new Author(1, "Jane Doe")
            });
            _mockDatabase.Setup(db => db.Books).Returns(new List<Book>());
            _handler = new AddBookCommandHandler(_mockDatabase.Object);
        }

        [Test]
        public async Task HandleShouldAddBookToDatabase()
        {
            // Arrange
            var command = new AddBookCommand("Book Name", "Book Title", "Description", 1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.Name, Is.EqualTo("Book Name"), "Book name should match input command");
            Assert.That(result.Author.Id, Is.EqualTo(1), "Author ID should match the input AuthorId");
            Assert.That(_mockDatabase.Object.Books, Contains.Item(result), "Resulting book should exist in the database");
        }
        [Test]
        public void Handle_ShouldThrowKeyNotFoundException_WhenAuthorDoesNotExist()
        {
            // Arrange
            var command = new AddBookCommand("Book Name", "Book Title", "Description", 99); // Non-existent author
            _mockDatabase.Setup(db => db.Authors).Returns(new List<Author>()); // Empty authors

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _handler.Handle(command, CancellationToken.None),
                "Expected KeyNotFoundException when author is not found"
            );
        }
        [Test]
        public void Handle_ShouldThrowKeyNotFoundException_WhenAuthorIdIsInvalid()
        {
            // Arrange
            var command = new AddBookCommand("Book Name", "Title", "Description", 999); // Invalid AuthorId
            _mockDatabase.Setup(db => db.Authors).Returns(new List<Author> { new Author(1, "Author Name") });

            // Act & Assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () =>
                await _handler.Handle(command, CancellationToken.None),
                "Expected KeyNotFoundException when author ID is invalid"
            );
        }
    }
}
