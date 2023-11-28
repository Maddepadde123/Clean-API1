using Application.Commands.Birds;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;

namespace Application.Tests.Commands.Birds
{
    [TestFixture]
    public class AddBirdCommandHandlerTests
    {
        private AddBirdCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new AddBirdCommandHandler(new MockDatabase());
        }

        [Test]
        public async Task Handle_AddsBirdToDatabase()
        {
            // Arrange
            var newBird = new BirdDto { Name = "NewBirdName" };
            var command = new AddBirdCommand(newBird);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<Bird>(result);

            // Kontrollera att hunden har fått ett giltigt ID
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));

            // Kontrollera att hunden har rätt namn enligt det som skickades med kommandot
            Assert.That(result.Name, Is.EqualTo("NewBirdName"));
        }
    }
}