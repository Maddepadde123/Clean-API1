using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Commands.Birds
{
    [TestFixture]
    public class UpdateBirdByIdCommandHandlerTests
    {
        private UpdateBirdByIdCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            var mockAnimalRepository = new Mock<IAnimalRepository>();
            _handler = new UpdateBirdByIdCommandHandler(mockAnimalRepository.Object);
        }

        [Test]
        public async Task Handle_UpdateBirdInDatabase()
        {
            // Arrange
            var birdId = Guid.NewGuid();
            var updatedBirdDto = new BirdDto { Name = "UpdatedBirdName", CanFly = true, Color = "Green" };
            var command = new UpdateBirdByIdCommand(updatedBirdDto, birdId);

            var existingBird = new Bird { Id = birdId, Name = "OriginalBirdName", CanFly = false, Color = "Blue" };

            var animalRepositoryMock = new Mock<IAnimalRepository>();
            animalRepositoryMock.Setup(repo => repo.GetBirdById(birdId)).ReturnsAsync(existingBird);

            _handler = new UpdateBirdByIdCommandHandler(animalRepositoryMock.Object);

            // Act
            var updatedBird = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(updatedBird);
            Assert.IsInstanceOf<Bird>(updatedBird);
            Assert.That(updatedBirdDto.Name, Is.EqualTo(updatedBird.Name));
            Assert.That(updatedBirdDto.CanFly, Is.EqualTo(updatedBird.CanFly));
            Assert.That(updatedBirdDto.Color, Is.EqualTo(updatedBird.Color));

            // Ensure that the repository's UpdateBirdById method was called with the correct arguments
            animalRepositoryMock.Verify(repo => repo.UpdateBirdById(It.IsAny<Bird>()), Times.Once);
        }
    }
}
