using Application.Animals.Queries.Cats.GetByWeightBreed;
using Application.Queries.Cats.GetCatsByWeightAndBreed;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Queries.Cats.GetByWeightBreed
{
    [TestFixture]
    public class GetCatsByWeightAndBreedQueryHandlerTests
    {
        private GetCatsByWeightAndBreedQueryHandler _handler;
        private Mock<IAnimalRepository> _mockAnimalRepository;
        private Mock<ILogger<GetCatsByWeightAndBreedQueryHandler>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockAnimalRepository = new Mock<IAnimalRepository>();
            _mockLogger = new Mock<ILogger<GetCatsByWeightAndBreedQueryHandler>>();
            _handler = new GetCatsByWeightAndBreedQueryHandler(_mockAnimalRepository.Object, _mockLogger.Object);
        }

        [Test]
        public async Task Handle_ReturnsListOfCats()
        {
            // Arrange
            var query = new GetCatsByWeightAndBreedQuery
            {
                CatBreed = "Siamese",
                CatWeight = 5
            };

            var expectedCats = new List<Cat>
            {
                new Cat { Id = Guid.NewGuid(), Name = "Cat1", CatBreed = "Siamese", CatWeight = 5 },
                new Cat { Id = Guid.NewGuid(), Name = "Cat2", CatBreed = "Siamese", CatWeight = 5 },
            };

            _mockAnimalRepository.Setup(repo => repo.GetCatsByWeightAndBreedAsync(query.CatBreed, query.CatWeight))
                .ReturnsAsync(expectedCats);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<List<Cat>>());
            Assert.That(result.Count, Is.EqualTo(expectedCats.Count));
        }
    }
}
