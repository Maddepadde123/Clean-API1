using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Commands.Cats
{
    [TestFixture]
    public class UpdateCatByIdCommandHandlerTests
    {
        private UpdateCatByIdCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            var mockAnimalRepository = new Mock<IAnimalRepository>();
            _handler = new UpdateCatByIdCommandHandler(mockAnimalRepository.Object);
        }

        [Test]
        public async Task Handle_UpdateCatInDatabase()
        {
            // Arrange
            var catId = Guid.NewGuid();
            var updatedCatDto = new CatDto
            {
                Name = "UpdatedCatName",
                LikesToPlay = true,
                CatBreed = "Siamese"
            };
            var command = new UpdateCatByIdCommand(updatedCatDto, catId);

            var existingCat = new Cat
            {
                Id = catId,
                Name = "OriginalCatName",
                LikesToPlay = false,
                CatBreed = "Persian"
            };

            var animalRepositoryMock = new Mock<IAnimalRepository>();
            animalRepositoryMock.Setup(repo => repo.GetCatById(catId)).ReturnsAsync(existingCat);

            _handler = new UpdateCatByIdCommandHandler(animalRepositoryMock.Object);

            // Act
            var updatedCat = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(updatedCat);
            Assert.IsInstanceOf<Cat>(updatedCat);
            Assert.That(updatedCatDto.Name, Is.EqualTo(updatedCat.Name));
            Assert.That(updatedCatDto.LikesToPlay, Is.EqualTo(updatedCat.LikesToPlay));
            Assert.That(updatedCatDto.CatBreed, Is.EqualTo(updatedCat.CatBreed));

            // Ensure that the repository's UpdateCatById method was called with the correct arguments
            animalRepositoryMock.Verify(repo => repo.UpdateCatById(It.IsAny<Cat>()), Times.Once);
        }
    }
}
