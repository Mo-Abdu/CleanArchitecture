using Application.Commands.AuthorCommands;
using Application.Handlers.AuthorHandlers;
using Infrastructure;
using Models;
using Moq;
namespace Test
{
    [TestFixture]
    public class AddAuthorCommandHandlerTests
    {
        private Mock<IFakeDatabase> _mockDatabase;
        private AddAuthorCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new Mock<IFakeDatabase>();
            _mockDatabase.Setup(db => db.Authors).Returns(new List<Author>());
            _handler = new AddAuthorCommandHandler(_mockDatabase.Object);
        }

        [Test]
        public async Task HandleShouldAddAuthorToDatabase()
        {
            // Arrange
            var command = new AddAuthorCommand("Omer Abu");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.Name, Is.EqualTo("Omer Abu"), "Author name should match input command");
            Assert.That(_mockDatabase.Object.Authors, Contains.Item(result), "Resulting author should exist in the database");
        }
    }
}
