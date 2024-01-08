using Application.Queries.Dogs.GetDogsByWeightAndBreed;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Queries.Dogs
{
    [TestFixture]
    public class GetDogsByWeightAndBreedQueryHandlerTests
    {
        private GetDogsByWeightAndBreedQueryHandler _handler;
        private Mock<IAnimalRepository> _mockAnimalRepository;
        private Mock<ILogger<GetDogsByWeightAndBreedQueryHandler>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockAnimalRepository = new Mock<IAnimalRepository>();
            _mockLogger = new Mock<ILogger<GetDogsByWeightAndBreedQueryHandler>>();
            _handler = new GetDogsByWeightAndBreedQueryHandler(_mockAnimalRepository.Object, _mockLogger.Object);
        }

        [Test]
        public async Task Handle_ReturnsListOfDogs()
        {
            // Arrange
            var query = new GetDogsByWeightAndBreedQuery
            {
                DogBreed = "Labrador",
                DogWeight = 20
            };

            var expectedDogs = new List<Dog>
            {
                new Dog { Id = Guid.NewGuid(), Name = "Dog1", DogBreed = "Labrador", DogWeight = 20 },
                new Dog { Id = Guid.NewGuid(), Name = "Dog2", DogBreed = "Labrador", DogWeight = 20 },
            };

            _mockAnimalRepository.Setup(repo => repo.GetDogsByWeightAndBreedAsync(query.DogBreed, query.DogWeight))
                .ReturnsAsync(expectedDogs);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<Dog>>());
            Assert.That(result.Count, Is.EqualTo(expectedDogs.Count));
        }
    }
}
