using Application.Queries.Users.GetAnimalUserById;
using Domain.Models.AnimalUser;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Queries.Users
{
    [TestFixture]
    public class GetAnimalUserByIdQueryHandlerTests
    {
        private GetAnimalUserByIdQueryHandler _handler;
        private Mock<IAnimalUserRepository> _mockAnimalUserRepository;

        [SetUp]
        public void Setup()
        {
            _mockAnimalUserRepository = new Mock<IAnimalUserRepository>();
            _handler = new GetAnimalUserByIdQueryHandler(_mockAnimalUserRepository.Object);
        }

        [Test]
        public async Task Handle_ReturnsAnimalUserById()
        {
            // Arrange
            var query = new GetAnimalUserByIdQuery(Guid.NewGuid(), Guid.NewGuid());

            var expectedAnimalUser = new AnimalUserModel
            {
                UserId = query.UserId,
                AnimalId = query.AnimalId,
            };

            _mockAnimalUserRepository.Setup(repo => repo.GetAnimalUserById(query.UserId, query.AnimalId))
                .ReturnsAsync(expectedAnimalUser);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedAnimalUser.UserId, result.UserId);
            Assert.AreEqual(expectedAnimalUser.AnimalId, result.AnimalId);
            _mockAnimalUserRepository.Verify(repo => repo.GetAnimalUserById(query.UserId, query.AnimalId), Times.Once);
        }

        [Test]
        public void Handle_ThrowsExceptionOnRepositoryError()
        {
            // Arrange
            var query = new GetAnimalUserByIdQuery(Guid.NewGuid(), Guid.NewGuid());

            _mockAnimalUserRepository.Setup(repo => repo.GetAnimalUserById(query.UserId, query.AnimalId))
                .ThrowsAsync(new Exception("Simulated repository error"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _handler.Handle(query, CancellationToken.None));
            _mockAnimalUserRepository.Verify(repo => repo.GetAnimalUserById(query.UserId, query.AnimalId), Times.Once);
        }
    }
}
