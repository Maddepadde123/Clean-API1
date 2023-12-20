using Application.Queries.Dogs.GetById;
using Domain.Models;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Queries.Dogs.GetById
{
    [TestFixture]
    public class GetDogByIdQueryHandlerTests
    {
        private GetDogByIdQueryHandler _handler;
        private Mock<IAnimalRepository> _mockAnimalRepository;

        [SetUp]
        public void Setup()
        {
            _mockAnimalRepository = new Mock<IAnimalRepository>();
            _handler = new GetDogByIdQueryHandler(_mockAnimalRepository.Object);
        }

        [Test]
        public async Task Handle_ReturnsCorrectDog()
        {
            // Arrange
            var dogId = Guid.NewGuid();
            var query = new GetDogByIdQuery(dogId);

            var expectedDog = new Dog
            {
                Id = dogId,
                Name = "MockDog",
            };

            _mockAnimalRepository.Setup(repo => repo.GetDogById(dogId))
                .ReturnsAsync(expectedDog);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<Dog>(result);
            Assert.AreEqual(expectedDog.Id, result.Id);
            Assert.AreEqual(expectedDog.Name, result.Name);
        }
    }
}
