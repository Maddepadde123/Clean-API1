using Application.Commands.Users.UpdateAnimalUser;
using Application.Dtos;
using Domain.Models.AnimalUser;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Commands.Users
{
    [TestFixture]
    public class UpdateAnimalUserByIdCommandHandlerTests
    {
        private UpdateAnimalUserByIdCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            var mockAnimalUserRepository = new Mock<IAnimalUserRepository>();
            _handler = new UpdateAnimalUserByIdCommandHandler(mockAnimalUserRepository.Object);
        }

        [Test]
        public async Task Handle_UpdateAnimalUserInDatabase()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalId = Guid.NewGuid();

            var updatedAnimalUserDto = new AnimalUserDto
            {
                UserId = userId,
                AnimalId = animalId,
            };

            var command = new UpdateAnimalUserByIdCommand(updatedAnimalUserDto, userId, animalId);

            var existingAnimalUser = new AnimalUserModel
            {
                UserId = userId,
                AnimalId = animalId,
            };

            var animalUserRepositoryMock = new Mock<IAnimalUserRepository>();
            animalUserRepositoryMock.Setup(repo => repo.GetAnimalUserById(userId, animalId)).ReturnsAsync(existingAnimalUser);

            _handler = new UpdateAnimalUserByIdCommandHandler(animalUserRepositoryMock.Object);

            // Act
            var updatedAnimalUser = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(updatedAnimalUser);
            Assert.IsInstanceOf<AnimalUserModel>(updatedAnimalUser);

            // Ensure that the repository's UpdateAnimalUser method was called with the correct arguments
            animalUserRepositoryMock.Verify(repo => repo.UpdateAnimalUser(It.IsAny<AnimalUserModel>()), Times.Once);
        }

        [Test]
        public void Handle_AnimalUserNotFound_ThrowsException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var animalId = Guid.NewGuid();

            var updatedAnimalUserDto = new AnimalUserDto
            {
                UserId = userId,
                AnimalId = animalId,
            };

            var command = new UpdateAnimalUserByIdCommand(updatedAnimalUserDto, userId, animalId);

            var animalUserRepositoryMock = new Mock<IAnimalUserRepository>();
            animalUserRepositoryMock.Setup(repo => repo.GetAnimalUserById(userId, animalId)).ReturnsAsync((AnimalUserModel)null);

            _handler = new UpdateAnimalUserByIdCommandHandler(animalUserRepositoryMock.Object);

            // Act & Assert
            Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
