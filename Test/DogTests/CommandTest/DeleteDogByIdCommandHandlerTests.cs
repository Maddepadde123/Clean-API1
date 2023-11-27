using Application.Commands.Dogs.DeleteDog;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;

namespace Application.Tests.Commands.Dogs
{
    [TestFixture]
    public class DeleteDogByIdCommandHandlerTests
    {
        private DeleteDogByIdCommandHandler _handler;
        private MockDatabase _mockDatabase;

        [SetUp]
        public void Setup()
        {
            _mockDatabase = new MockDatabase();
            _handler = new DeleteDogByIdCommandHandler(_mockDatabase);
        }

        [Test]
        public async Task Handle_DeletesDogInDatabase()
        {
            // Arrange
            var initialDog = new Dog { Id = Guid.NewGuid(), Name = "InitialDogName" };
            _mockDatabase.Dogs.Add(initialDog);

            // Create an instance of DeleteDogByIdCommand
            var command = new DeleteDogByIdCommand(
                deletedDog: new DogDto { Name = "InitialDogName" },
                deletedDogId: initialDog.Id
            );

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);

            // Check that the dog has been deleted from MockDatabase
            var deletedDogInDatabase = _mockDatabase.Dogs.FirstOrDefault(dog => dog.Id == command.DeletedDogId);
            Assert.IsNull(deletedDogInDatabase);
        }
    }
}
