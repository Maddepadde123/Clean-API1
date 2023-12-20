using Application.Queries.Users.GetById;
using Domain.Models.User;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

[TestFixture]
public class GetUserByIdQueryHandlerTests
{
    private GetUserByIdQueryHandler _handler;
    private Mock<IUserRepository> _mockUserRepository;

    [SetUp]
    public void Setup()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _handler = new GetUserByIdQueryHandler(_mockUserRepository.Object);
    }

    [Test]
    public async Task Handle_ReturnsCorrectUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var query = new GetUserByIdQuery(userId);

        var expectedUser = new UserModel
        {
            Id = userId,
            UserName = "TestUser",
            UserPassword = "TestPassword",
        };

        _mockUserRepository.Setup(repo => repo.GetUserById(userId))
            .ReturnsAsync(expectedUser);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsInstanceOf<UserModel>(result);
        Assert.AreEqual(expectedUser.Id, result.Id);
        Assert.AreEqual(expectedUser.UserName, result.UserName);
        Assert.AreEqual(expectedUser.UserPassword, result.UserPassword);

        // Ensure that the repository's GetUserById method was called
        _mockUserRepository.Verify(repo => repo.GetUserById(userId), Times.Once);
    }

    [Test]
    public async Task Handle_UserNotFound_ReturnsNull()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var query = new GetUserByIdQuery(userId);

        _mockUserRepository.Setup(repo => repo.GetUserById(userId))
            .ReturnsAsync((UserModel)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(result);

        // Ensure that the repository's GetUserById method was called
        _mockUserRepository.Verify(repo => repo.GetUserById(userId), Times.Once);
    }
}
