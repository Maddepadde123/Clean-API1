using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;

namespace Application.Tests.Commands.Cats
{
    [TestFixture]
    public class UpdateCatByIdCommandHandlerTests
    {
        private UpdateCatByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new MockDatabase();
            _handler = new UpdateCatByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_UpdatesCatInDatabase()
        {
            // Arrange
            var initialCat = new Cat { Id = Guid.NewGuid(), Name = "InitialCatName", LikesToPlay = true };
            _mockDatabase.Cats.Add(initialCat);

            // Create an instance of UpdateCatByIdCommand
            var command = new UpdateCatByIdCommand(
                updatedCat: new CatDto { Name = "UpdatedCatName", LikesToPlay = false },
                id: initialCat.Id
            );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<Cat>(result);

            // Check that the cat has the correct updated name
            Assert.That(result.Name, Is.EqualTo("UpdatedCatName"));

            // Check that the cat has been updated in MockDatabase
            var updatedCatInDatabase = _mockDatabase.Cats.FirstOrDefault(cat => cat.Id == command.Id);
            Assert.That(updatedCatInDatabase, Is.Not.Null);
            Assert.That(updatedCatInDatabase.Name, Is.EqualTo("UpdatedCatName"));
            Assert.That(updatedCatInDatabase.LikesToPlay, Is.EqualTo(false));
        }
    }
}
