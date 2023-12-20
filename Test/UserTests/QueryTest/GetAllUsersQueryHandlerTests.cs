using Application.Queries.Users;
using Application.Queries.Users.GetAll;
using Domain.Models.User;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Queries.Users
{
    [TestFixture]
    public class GetAllUsersQueryHandlerTests
    {
        private GetAllUsersQueryHandler _handler;
        private Mock<IUserRepository> _mockUserRepository;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _handler = new GetAllUsersQueryHandler(_mockUserRepository.Object);
        }

        [Test]
        public async Task Handle_ReturnsAllUsersFromRepository()
        {
            // Arrange
            var expectedUsers = new List<UserModel>
            {
                new UserModel { Id = Guid.NewGuid(), UserName = "User1", UserPassword = "Password1" },
                new UserModel { Id = Guid.NewGuid(), UserName = "User2", UserPassword = "Password2" },
            };

            _mockUserRepository.Setup(repo => repo.GetAllUsers())
                .ReturnsAsync(expectedUsers);

            var query = new GetAllUsersQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<List<UserModel>>(result);
        }
    }
}
