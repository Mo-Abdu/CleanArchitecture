using Application.Commands.AuthorCommands;
using Application.Handlers.AuthorHandlers;
using Infrastructure;
using Models;
using Moq;
namespace Test
{
    [TestFixture]
    public class UpdateAuthorCommandHandlerTests
    {
        private Mock<IFakeDatabase> _mockDatabase;
        private UpdateAuthorCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new Mock<IFakeDatabase>();
            _mockDatabase.Setup(db => db.Authors).Returns(new List<Author>
            {
                new Author(1, "Jane Doe")
            });
            _handler = new UpdateAuthorCommandHandler(_mockDatabase.Object);
        }

        [Test]
        public async Task HandleShouldUpdateAuthorInDatabase()
        {
            // Arrange
            var command = new UpdateAuthorCommand(1, "John Smith");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.Name, Is.EqualTo("John Smith"), "Author name should be updated");
            Assert.That(_mockDatabase.Object.Authors.First(a => a.Id == 1).Name, Is.EqualTo("John Smith"), "Database author should be updated");
        }
    }
}
