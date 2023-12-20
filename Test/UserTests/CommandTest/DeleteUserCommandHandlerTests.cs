using Application.Commands.Users.DeleteUser;
using Domain.Models.User;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Commands.Users
{
    [TestFixture]
    public class DeleteUserByIdCommandHandlerTests
    {
        private DeleteUserByIdCommandHandler _handler;
        private Mock<IUserRepository> _mockUserRepository;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _handler = new DeleteUserByIdCommandHandler(_mockUserRepository.Object);
        }

        [Test]
        public async Task Handle_DeletesUserInRepository()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var deleteUserCommand = new DeleteUserByIdCommand(userId);

            var userToDelete = new UserModel
            {
                Id = userId,
                UserName = "UserToDelete"
            };

            _mockUserRepository.Setup(repo => repo.GetUserById(userId))
                .ReturnsAsync(userToDelete);

            // Act
            var result = await _handler.Handle(deleteUserCommand, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);

            // Verify that the repository's DeleteUserById method was called with the correct user ID
            _mockUserRepository.Verify(repo => repo.DeleteUserById(userId), Times.Once);
        }

        [Test]
        public async Task Handle_ReturnsFalseIfUserNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var deleteUserCommand = new DeleteUserByIdCommand(userId);

            _mockUserRepository.Setup(repo => repo.GetUserById(userId))
                .ReturnsAsync((UserModel)null);

            // Act
            var result = await _handler.Handle(deleteUserCommand, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);

            // Verify that the repository's DeleteUserById method was not called
            _mockUserRepository.Verify(repo => repo.DeleteUserById(It.IsAny<Guid>()), Times.Never);
        }

        [Test]
        public async Task Handle_ReturnsFalseOnException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var deleteUserCommand = new DeleteUserByIdCommand(userId);

            _mockUserRepository.Setup(repo => repo.GetUserById(userId))
                .ThrowsAsync(new Exception("Simulated exception"));

            // Act
            var result = await _handler.Handle(deleteUserCommand, CancellationToken.None);

            // Assert
            Assert.IsFalse(result);

            // Verify that the repository's DeleteUserById method was not called
            _mockUserRepository.Verify(repo => repo.DeleteUserById(It.IsAny<Guid>()), Times.Never);
        }
    }
}
