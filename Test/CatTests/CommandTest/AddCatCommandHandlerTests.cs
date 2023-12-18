//using Application.Commands.Cats;
//using Application.Dtos;
//using Domain.Models;
//using Infrastructure.Database;

//namespace Application.Tests.Commands.Cats
//{
//    [TestFixture]
//    public class AddCatCommandHandlerTests
//    {
//        private AddCatCommandHandler _handler;

//        [SetUp]
//        public void Setup()
//        {
//            _handler = new AddCatCommandHandler(new MockDatabase());
//        }

//        [Test]
//        public async Task Handle_AddsCatToDatabase()
//        {
//            // Arrange
//            var newCat = new CatDto { Name = "NewCatName" };
//            var command = new AddCatCommand(newCat);

//            // Act
//            var result = await _handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.NotNull(result);
//            Assert.IsInstanceOf<Cat>(result);

//            // Kontrollera att katten har fått ett giltigt ID
//            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));

//            // Kontrollera att katten har rätt namn enligt det som skickades med kommandot
//            Assert.That(result.Name, Is.EqualTo("NewCatName"));
//        }
//    }
//}