using Application.Queries.Birds;
using Application.Queries.Birds.GetAll;
using Domain.Models;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Queries.Birds
{
    [TestFixture]
    public class GetAllBirdsQueryHandlerTests
    {
        private GetAllBirdsQueryHandler _handler;
        private Mock<IAnimalRepository> _mockAnimalRepository;

        [SetUp]
        public void Setup()
        {
            _mockAnimalRepository = new Mock<IAnimalRepository>();
            _handler = new GetAllBirdsQueryHandler(_mockAnimalRepository.Object);
        }

        [Test]
        public async Task Handle_ReturnsAllBirdsFromRepository()
        {
            // Arrange
            var expectedBirds = new List<Bird>
            {
                new Bird { Id = Guid.NewGuid(), Name = "Bird1", CanFly = true },
                new Bird { Id = Guid.NewGuid(), Name = "Bird2", CanFly = false }
            };

            _mockAnimalRepository.Setup(repo => repo.GetAllBirds()).ReturnsAsync(expectedBirds);

            // Act
            var result = await _handler.Handle(new GetAllBirdsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<List<Bird>>(result);
            Assert.AreEqual(expectedBirds.Count, result.Count);
        }
    }
}
