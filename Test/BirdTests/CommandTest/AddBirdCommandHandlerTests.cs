using Application.Commands.Birds;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Commands.Birds
{
    [TestFixture]
    public class AddBirdCommandHandlerTests
    {
        private AddBirdCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            var mockAnimalRepository = new Mock<IAnimalRepository>();
            _handler = new AddBirdCommandHandler(mockAnimalRepository.Object);
        }

        [Test]
        public async Task Handle_AddsBirdToDatabase()
        {
            // Arrange
            var newBird = new BirdDto { Name = "NewBirdName", CanFly = true, Color = "Blue" };
            var command = new AddBirdCommand(newBird);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<Bird>(result);
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(result.Name, Is.EqualTo(newBird.Name));
        }

    }
}
