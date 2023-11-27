using Application.Commands.Dogs;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Database;

namespace Application.Tests.Commands.Dogs
{
    [TestFixture]
    public class AddDogCommandHandlerTests
    {
        private AddDogCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            // Skapa en instans av AddDogCommandHandler med en mock av MockDatabase
            _handler = new AddDogCommandHandler(new MockDatabase());
        }

        [Test]
        public async Task Handle_AddsDogToDatabase()
        {
            // Arrange
            var newDog = new DogDto { Name = "NewDogName" };
            var command = new AddDogCommand(newDog);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<Dog>(result);

            // Kontrollera att hunden har fått ett giltigt ID
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));

            // Kontrollera att hunden har rätt namn enligt det som skickades med kommandot
            Assert.That(result.Name, Is.EqualTo("NewDogName"));
        }
    }
}