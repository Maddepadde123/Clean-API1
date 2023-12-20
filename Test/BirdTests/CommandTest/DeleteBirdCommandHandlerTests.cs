using Application.Commands.Birds.DeleteDog;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Commands.Birds
{
    [TestFixture]
    public class DeleteBirdCommandHandlerTests
    {
        private DeleteBirdByIdCommandHandler _handler;
        private Mock<IAnimalRepository> _mockAnimalRepository;

        [SetUp]
        public void Setup()
        {
            _mockAnimalRepository = new Mock<IAnimalRepository>();
            _handler = new DeleteBirdByIdCommandHandler(_mockAnimalRepository.Object);
        }

        [Test]
        public async Task Handle_DeletesBirdFromDatabase()
        {
            // Arrange
            var birdIdToDelete = Guid.NewGuid();
            var command = new DeleteBirdByIdCommand(birdIdToDelete);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            // Verify that the DeleteBirdById method was called with the correct birdIdToDelete
            _mockAnimalRepository.Verify(repo => repo.DeleteBirdById(birdIdToDelete), Times.Once);
        }
    }
}
