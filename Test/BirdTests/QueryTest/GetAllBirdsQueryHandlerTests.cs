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
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<Bird>>());
            Assert.That(result.Count, Is.EqualTo(expectedBirds.Count));

            // Ensure that the repository's GetAllBirds method was called
            _mockAnimalRepository.Verify(repo => repo.GetAllBirds(), Times.Once);
        }
    }
}
