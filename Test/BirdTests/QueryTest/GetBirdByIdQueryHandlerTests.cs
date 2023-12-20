using Application.Queries.Birds.GetById;
using Domain.Models;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Queries.Birds.GetById
{
    [TestFixture]
    public class GetBirdByIdQueryHandlerTests
    {
        private GetBirdByIdQueryHandler _handler;
        private Mock<IAnimalRepository> _mockAnimalRepository;

        [SetUp]
        public void Setup()
        {
            _mockAnimalRepository = new Mock<IAnimalRepository>();
            _handler = new GetBirdByIdQueryHandler(_mockAnimalRepository.Object);
        }

        [Test]
        public async Task Handle_ReturnsCorrectBird()
        {
            // Arrange
            var birdId = Guid.NewGuid();
            var query = new GetBirdByIdQuery(birdId);

            var expectedBird = new Bird
            {
                Id = birdId,
                Name = "MockBird",
                CanFly = true,
            };

            _mockAnimalRepository.Setup(repo => repo.GetBirdById(birdId))
                .ReturnsAsync(expectedBird);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Bird>());
            Assert.That(result.Id, Is.EqualTo(expectedBird.Id));
            Assert.That(result.Name, Is.EqualTo(expectedBird.Name));
            Assert.That(result.CanFly, Is.EqualTo(expectedBird.CanFly));
        }
    }
}
