using Application.Commands.Users.DeleteAnimalUser;
using Domain.Models.AnimalUser;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Commands.Users.DeleteAnimalUser
{
    [TestFixture]
    public class DeleteAnimalUserByIdCommandHandlerTests
    {
        private DeleteAnimalUserByIdCommandHandler _handler;
        private Mock<IAnimalUserRepository> _mockAnimalUserRepository;

        [SetUp]
        public void Setup()
        {
            _mockAnimalUserRepository = new Mock<IAnimalUserRepository>();
            _handler = new DeleteAnimalUserByIdCommandHandler(_mockAnimalUserRepository.Object);
        }

        [Test]
        public async Task Handle_ReturnsTrueOnSuccessfulDeletion()
        {
            // Arrange
            var command = new DeleteAnimalUserByIdCommand(Guid.NewGuid(), Guid.NewGuid());

            var animalUserToDelete = new AnimalUserModel
            {
                UserId = command.UserId,
                AnimalId = command.AnimalId,
            };

            _mockAnimalUserRepository.Setup(repo => repo.GetAnimalUserById(command.UserId, command.AnimalId))
                .ReturnsAsync(animalUserToDelete);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);
            _mockAnimalUserRepository.Verify(repo => repo.DeleteAnimalUserById(command.UserId, command.AnimalId), Times.Once);
        }

        [Test]
        public async Task Handle_ReturnsFalseOnNonexistentAnimalUser()
        {
            // Arrange
            var command = new DeleteAnimalUserByIdCommand(Guid.NewGuid(), Guid.NewGuid());

            _mockAnimalUserRepository.Setup(repo => repo.GetAnimalUserById(command.UserId, command.AnimalId))
                .ReturnsAsync((AnimalUserModel)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);
            _mockAnimalUserRepository.Verify(repo => repo.DeleteAnimalUserById(command.UserId, command.AnimalId), Times.Never);
        }
    }
}
