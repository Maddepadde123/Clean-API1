using Application.Queries.Cats;
using Application.Queries.Cats.GetById;
using Domain.Models;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Queries.Cats.GetById
{
    [TestFixture]
    public class GetCatByIdQueryHandlerTests
    {
        private GetCatByIdQueryHandler _handler;
        private Mock<IAnimalRepository> _mockAnimalRepository;

        [SetUp]
        public void Setup()
        {
            _mockAnimalRepository = new Mock<IAnimalRepository>();
            _handler = new GetCatByIdQueryHandler(_mockAnimalRepository.Object);
        }

        [Test]
        public async Task Handle_ReturnsCorrectCat()
        {
            // Arrange
            var catId = Guid.NewGuid();
            var query = new GetCatByIdQuery(catId);
            var expectedCat = new Cat
            {
                Id = catId,
                Name = "MockCat",
            };

            _mockAnimalRepository.Setup(repo => repo.GetCatById(catId))
                .ReturnsAsync(expectedCat);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<Cat>());
            Assert.That(result.Id, Is.EqualTo(expectedCat.Id));
            Assert.That(result.Name, Is.EqualTo(expectedCat.Name));
        }
    }
}
