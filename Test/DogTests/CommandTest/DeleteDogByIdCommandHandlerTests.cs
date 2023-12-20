using Application.Commands.Dogs.DeleteDog;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Commands.Dogs
{
    [TestFixture]
    public class DeleteDogByIdCommandHandlerTests
    {
        private DeleteDogByIdCommandHandler _handler;
        private Mock<IAnimalRepository> _mockAnimalRepository;

        [SetUp]
        public void Setup()
        {
            _mockAnimalRepository = new Mock<IAnimalRepository>();
            _handler = new DeleteDogByIdCommandHandler(_mockAnimalRepository.Object);
        }

        [Test]
        public async Task Handle_DeletesDogFromDatabase()
        {
            // Arrange
            var dogIdToDelete = Guid.NewGuid();
            var command = new DeleteDogByIdCommand(dogIdToDelete);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            // Verify that the DeleteDogById method was called with the correct dogIdToDelete
            _mockAnimalRepository.Verify(repo => repo.DeleteDogById(dogIdToDelete), Times.Once);
        }
    }
}
