using Application.Commands.Users.UpdateUser;
using Application.Dtos;
using Domain.Models.User;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

[TestFixture]
public class UpdateUserByIdCommandHandlerTests
{
    private UpdateUserByIdCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        var mockUserRepository = new Mock<IUserRepository>();
        _handler = new UpdateUserByIdCommandHandler(mockUserRepository.Object);
    }

    [Test]
    public async Task Handle_UpdateUserInDatabase()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var updatedUserDto = new UserDto
        {
            Id = userId,
            Username = "UpdatedUserName"
            // Add other updated properties here as needed
        };

        var command = new UpdateUserByIdCommand(updatedUserDto, userId);

        var existingUser = new UserModel
        {
            Id = userId,
            UserName = "OriginalUserName",
            UserPassword = "OriginalPassword"
            // Add other existing properties here as needed
        };

        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(repo => repo.GetUserById(userId)).ReturnsAsync(existingUser);

        _handler = new UpdateUserByIdCommandHandler(userRepositoryMock.Object);

        // Act
        var updatedUser = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(updatedUser);
        Assert.IsInstanceOf<UserModel>(updatedUser);
        Assert.AreEqual(updatedUserDto.Username, updatedUser.UserName);
        // Add assertions for other properties as needed

        // Ensure that the repository's UpdateUserById method was called with the correct arguments
        userRepositoryMock.Verify(repo => repo.UpdateUserById(It.IsAny<UserModel>()), Times.Once);
    }

    [Test]
    public void Handle_UserNotFound_ThrowsException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var updatedUserDto = new UserDto
        {
            Id = userId,
            Username = "UpdatedUserName"
        };

        var command = new UpdateUserByIdCommand(updatedUserDto, userId);

        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(repo => repo.GetUserById(userId)).ReturnsAsync((UserModel)null);

        _handler = new UpdateUserByIdCommandHandler(userRepositoryMock.Object);

        // Act & Assert
        Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
