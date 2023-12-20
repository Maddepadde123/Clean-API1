using Application.Commands.Users.AddAnimalUser;
using Application.Dtos;
using Domain.Models.AnimalUser;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Commands.Users.AddAnimalUser
{
    [TestFixture]
    public class AddAnimalUserCommandHandlerTests
    {
        private AddAnimalUserCommandHandler _handler;
        private Mock<IAnimalUserRepository> _mockAnimalUserRepository;

        [SetUp]
        public void Setup()
        {
            _mockAnimalUserRepository = new Mock<IAnimalUserRepository>();
            _handler = new AddAnimalUserCommandHandler(_mockAnimalUserRepository.Object);
        }

        [Test]
        public async Task Handle_ReturnsCorrectAnimalUser()
        {
            // Arrange
            var command = new AddAnimalUserCommand(new AnimalUserDto
            {
                UserId = Guid.NewGuid(),
                AnimalId = Guid.NewGuid(),
            });

            var expectedAnimalUser = new AnimalUserModel
            {
                UserId = command.UserId,
                AnimalId = command.AnimalId,
            };

            _mockAnimalUserRepository.Setup(repo => repo.AddAnimalUserAsync(It.IsAny<AnimalUserModel>()))
                .ReturnsAsync(expectedAnimalUser);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<AnimalUserModel>(result);
            Assert.AreEqual(expectedAnimalUser.UserId, result.UserId);
            Assert.AreEqual(expectedAnimalUser.AnimalId, result.AnimalId);
        }
    }
}
