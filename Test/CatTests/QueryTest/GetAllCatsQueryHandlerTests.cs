using Application.Queries.Cats;
using Application.Queries.Cats.GetAll;
using Domain.Models;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Queries.Cats
{
    [TestFixture]
    public class GetAllCatsQueryHandlerTests
    {
        private GetAllCatsQueryHandler _handler;
        private Mock<IAnimalRepository> _mockAnimalRepository;

        [SetUp]
        public void Setup()
        {
            _mockAnimalRepository = new Mock<IAnimalRepository>();
            _handler = new GetAllCatsQueryHandler(_mockAnimalRepository.Object);
        }

        [Test]
        public async Task Handle_ReturnsAllCatsFromRepository()
        {
            // Arrange
            var expectedCats = new List<Cat>
            {
                new Cat { Id = Guid.NewGuid(), Name = "Cat1", LikesToPlay = true },
                new Cat { Id = Guid.NewGuid(), Name = "Cat2", LikesToPlay = false }
            };

            _mockAnimalRepository.Setup(repo => repo.GetAllCats()).ReturnsAsync(expectedCats);

            // Act
            var result = await _handler.Handle(new GetAllCatsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<List<Cat>>(result);
            Assert.AreEqual(expectedCats.Count, result.Count);
        }
    }
}