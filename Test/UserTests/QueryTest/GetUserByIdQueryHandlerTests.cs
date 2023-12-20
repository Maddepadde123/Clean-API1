using Application.Queries.Users.GetById;
using Domain.Models.User;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Queries.Users.GetById
{
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
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<UserModel>());
            Assert.That(result.Id, Is.EqualTo(expectedUser.Id));
            Assert.That(result.UserName, Is.EqualTo(expectedUser.UserName));
            Assert.That(result.UserPassword, Is.EqualTo(expectedUser.UserPassword));
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
            Assert.That(result, Is.Null);
        }
    }
}
