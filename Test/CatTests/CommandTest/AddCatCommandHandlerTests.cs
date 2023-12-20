using Application.Commands.Cats;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Commands.Cats
{
    [TestFixture]
    public class AddCatCommandHandlerTests
    {
        private AddCatCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            var mockAnimalRepository = new Mock<IAnimalRepository>();
            _handler = new AddCatCommandHandler(mockAnimalRepository.Object);
        }

        [Test]
        public async Task Handle_AddsCatToDatabase()
        {
            // Arrange
            var newCat = new CatDto { Name = "NewCatName", LikesToPlay = true, CatBreed = "Persian", CatWeight = 5 };
            var command = new AddCatCommand(newCat);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<Cat>(result);
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(result.Name, Is.EqualTo(newCat.Name));
        }
    }
}
