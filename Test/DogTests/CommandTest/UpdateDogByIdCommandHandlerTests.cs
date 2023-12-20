using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Commands.Dogs
{
    [TestFixture]
    public class UpdateDogByIdCommandHandlerTests
    {
        private UpdateDogByIdCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            var mockAnimalRepository = new Mock<IAnimalRepository>();
            _handler = new UpdateDogByIdCommandHandler(mockAnimalRepository.Object);
        }

        [Test]
        public async Task Handle_UpdateDogInDatabase()
        {
            // Arrange
            var dogId = Guid.NewGuid();
            var updatedDogDto = new DogDto
            {
                Name = "UpdatedDogName",
                DogBreed = "Golden Retriever",
                DogWeight = 25
            };
            var command = new UpdateDogByIdCommand(updatedDogDto, dogId);

            var existingDog = new Dog
            {
                Id = dogId,
                Name = "OriginalDogName",
                DogBreed = "Labrador",
                DogWeight = 20
            };

            var animalRepositoryMock = new Mock<IAnimalRepository>();
            animalRepositoryMock.Setup(repo => repo.GetDogById(dogId)).ReturnsAsync(existingDog);

            _handler = new UpdateDogByIdCommandHandler(animalRepositoryMock.Object);

            // Act
            var updatedDog = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(updatedDog);
            Assert.IsInstanceOf<Dog>(updatedDog);
            Assert.That(updatedDogDto.Name, Is.EqualTo(updatedDog.Name));
            Assert.That(updatedDogDto.DogBreed, Is.EqualTo(updatedDog.DogBreed));
            Assert.That(updatedDogDto.DogWeight, Is.EqualTo(updatedDog.DogWeight).Within(0.001));
        }
    }
}
