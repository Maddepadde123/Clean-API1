//using Application.Commands.Birds.DeleteDog;
//using Domain.Models;
//using Infrastructure.Database;

//namespace Application.Tests.Commands.Birds
//{
//    [TestFixture]
//    public class DeleteBirdByIdCommandHandlerTests
//    {
//        private DeleteBirdByIdCommandHandler _handler;
//        private MockDatabase _mockDatabase;

//        [SetUp]
//        public void Setup()
//        {
//            _mockDatabase = new MockDatabase();
//            _handler = new DeleteBirdByIdCommandHandler(_mockDatabase);
//        }

//        [Test]
//        public async Task Handle_DeletesBirdInDatabase()
//        {
//            // Arrange
//            var initialBird = new Bird { Id = Guid.NewGuid(), Name = "InitialBirdName" };
//            _mockDatabase.Birds.Add(initialBird);

//            // Create an instance of DeleteBirdByIdCommand
//            var command = new DeleteBirdByIdCommand(
//                deletedBirdId: initialBird.Id
//            );

//            // Act
//            var result = await _handler.Handle(command, CancellationToken.None);

//            // Assert
//            Assert.IsTrue(result);
//        }
//    }
//}