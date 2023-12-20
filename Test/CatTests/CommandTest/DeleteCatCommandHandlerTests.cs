using Application.Commands.Cats.DeleteDog;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Commands.Cats
{
    [TestFixture]
    public class DeleteCatCommandHandlerTests
    {
        private DeleteCatByIdCommandHandler _handler;
        private Mock<IAnimalRepository> _mockAnimalRepository;

        [SetUp]
        public void Setup()
        {
            _mockAnimalRepository = new Mock<IAnimalRepository>();
            _handler = new DeleteCatByIdCommandHandler(_mockAnimalRepository.Object);
        }

        [Test]
        public async Task Handle_DeletesCatFromDatabase()
        {
            // Arrange
            var catIdToDelete = Guid.NewGuid();
            var command = new DeleteCatByIdCommand(catIdToDelete);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            // Verify that the DeleteCatById method was called with the correct catIdToDelete
            _mockAnimalRepository.Verify(repo => repo.DeleteCatById(catIdToDelete), Times.Once);
        }
    }
}
