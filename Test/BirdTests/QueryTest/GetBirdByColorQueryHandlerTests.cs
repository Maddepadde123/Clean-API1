using Application.Queries.Birds.GetBirdByColor;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Queries.Birds.GetBirdByColor
{
    [TestFixture]
    public class GetBirdByColorQueryHandlerTests
    {
        private GetBirdByColorQueryHandler _handler;
        private Mock<IAnimalRepository> _mockAnimalRepository;
        private Mock<ILogger<GetBirdByColorQueryHandler>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockAnimalRepository = new Mock<IAnimalRepository>();
            _mockLogger = new Mock<ILogger<GetBirdByColorQueryHandler>>();
            _handler = new GetBirdByColorQueryHandler(_mockAnimalRepository.Object, _mockLogger.Object);
        }

        [Test]
        public async Task Handle_ReturnsSortedBirds()
        {
            // Arrange
            var color = "Blue";
            var query = new GetBirdByColorQuery { Color = color };
            var expectedBirds = new List<Bird>
            {
                new Bird { Id = Guid.NewGuid(), Name = "Parrot" },
                new Bird { Id = Guid.NewGuid(), Name = "Eagle" },
            };

            _mockAnimalRepository.Setup(repo => repo.GetBirdsByColorAsync(color))
                .ReturnsAsync(expectedBirds);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<Bird>>());
            Assert.That(result, Is.Ordered.By("Name").Descending);
        }
    }
}
