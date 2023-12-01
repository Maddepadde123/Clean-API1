using Application.Commands.Cats.DeleteDog;
using Domain.Models;
using Infrastructure.Database;

namespace Application.Tests.Commands.Cats
{
    [TestFixture]
    public class DeleteCatByIdCommandHandlerTests
    {
        private DeleteCatByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new MockDatabase();
            _handler = new DeleteCatByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_DeletesCatInDatabase()
        {
            // Arrange
            var initialCat = new Cat { Id = Guid.NewGuid(), Name = "InitialCatName" };
            _mockDatabase.Cats.Add(initialCat);

            // Create an instance of DeleteCatByIdCommand
            var command = new DeleteCatByIdCommand(
                deletedCatId: initialCat.Id
            );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
        }
    }
}