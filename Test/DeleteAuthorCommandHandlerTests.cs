using Application.Commands.AuthorCommands;
using Application.Handlers.AuthorHandlers;
using Infrastructure;
using Models;
using Moq;
namespace Test
{
    [TestFixture]
    public class DeleteAuthorCommandHandlerTests
    {
        private Mock<IFakeDatabase> _mockDatabase;
        private DeleteAuthorCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new Mock<IFakeDatabase>();
            _mockDatabase.Setup(db => db.Authors).Returns(new List<Author>
            {
                new Author(1, "Jane Doe")
            });
            _handler = new DeleteAuthorCommandHandler(_mockDatabase.Object);
        }

        [Test]
        public async Task HandleShouldDeleteAuthorFromDatabase()
        {
            // Arrange
            var command = new DeleteAuthorCommand(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True, "Result should be true for successful deletion");
            Assert.That(_mockDatabase.Object.Authors, Is.Empty, "Authors list should be empty after deletion");
        }
    }
}
