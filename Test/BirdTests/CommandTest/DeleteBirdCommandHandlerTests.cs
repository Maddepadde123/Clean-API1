using Application.Commands.Birds.DeleteDog;
using Domain.Models;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Commands.Birds
{
    [TestFixture]
    public class DeleteBirdByIdCommandHandlerTests
    {
        private DeleteBirdByIdCommandHandler _handler;
        private Mock<IAnimalRepository> _animalRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _animalRepositoryMock = new Mock<IAnimalRepository>();
            _handler = new DeleteBirdByIdCommandHandler(_animalRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_DeletesBirdInRepository()
        {
            // Arrange
            var deletedBirdId = Guid.NewGuid();

            // Mocka upp en lista med fåglar i din repository
            var birdsInDatabase = new[] { new Bird { Id = deletedBirdId, Name = "DeletedBird" } }.AsQueryable();
            _animalRepositoryMock.Setup(repo => repo.GetAllBirds()).Returns((Task<List<Bird>>)birdsInDatabase);

            // Create an instance of DeleteBirdByIdCommand
            var command = new DeleteBirdByIdCommand(deletedBirdId);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsTrue(result);

            // Kolla att DeleteBirdById anropas med rätt ID
            _animalRepositoryMock.Verify(repo => repo.DeleteBirdById(deletedBirdId), Times.Once);
        }
    }
}
