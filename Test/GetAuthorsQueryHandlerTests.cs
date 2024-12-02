using Application.Queries;
using Infrastructure;
using Models;
using Moq;
namespace Test
{
    [TestFixture]
    public class GetAuthorsQueryHandlerTests
    {
        private Mock<IFakeDatabase> _mockDatabase;
        private GetAuthorsQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _mockDatabase = new Mock<IFakeDatabase>();
            _mockDatabase.Setup(db => db.Authors).Returns(new List<Author>
            {
                new Author(1, "Jane Doe"),
                new Author(2, "John Smith")
            });
            _handler = new GetAuthorsQueryHandler(_mockDatabase.Object);
        }

        [Test]
        public async Task HandleShouldReturnAllAuthors()
        {
            // Arrange
            var query = new GetAuthorsQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null, "Result should not be null");
            Assert.That(result.Count, Is.EqualTo(2), "Result count should match the number of authors in the database");
        }
    }
}
