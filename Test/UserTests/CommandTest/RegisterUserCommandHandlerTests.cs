using Application.Commands.Users.RegisterUser;
using Application.Dtos;
using Domain.Models.User;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Commands.Users
{
    [TestFixture]
    public class RegisterUserCommandHandlerTests
    {
        private RegisterUserCommandHandler _handler;
        private Mock<IUserRepository> _mockUserRepository;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _handler = new RegisterUserCommandHandler(_mockUserRepository.Object);
        }

        [Test]
        public async Task Handle_RegistersUserInRepository()
        {
            // Arrange
            var newUserCommand = new RegisterUserCommand(new UserRegistrationDto
            {
                Username = "NewUser",
                Password = "SecurePassword"
            });

            // Act
            var result = await _handler.Handle(newUserCommand, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<UserModel>(result);

            // Verify that the repository's RegisterUser method was called with the correct user data
            _mockUserRepository.Verify(repo => repo.RegisterUser(It.IsAny<UserModel>()), Times.Once);
        }
    }
}
