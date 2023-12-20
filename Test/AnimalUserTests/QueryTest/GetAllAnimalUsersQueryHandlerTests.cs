using Application.Queries.Users;
using Application.Queries.Users.GetAllAnimalUsers;
using Domain.Models.AnimalUser;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

[TestFixture]
public class GetAllAnimalUsersQueryHandlerTests
{
    private GetAllAnimalUsersQueryHandler _handler;
    private Mock<IAnimalUserRepository> _mockAnimalUserRepository;

    [SetUp]
    public void Setup()
    {
        _mockAnimalUserRepository = new Mock<IAnimalUserRepository>();
        _handler = new GetAllAnimalUsersQueryHandler(_mockAnimalUserRepository.Object);
    }

    [Test]
    public async Task Handle_ReturnsAllAnimalUsers()
    {
        // Arrange
        var query = new GetAllAnimalUsersQuery();

        var expectedAnimalUsers = new List<AnimalUserModel>
        {
            new AnimalUserModel { UserId = Guid.NewGuid(), AnimalId = Guid.NewGuid() },
            new AnimalUserModel { UserId = Guid.NewGuid(), AnimalId = Guid.NewGuid() },
        };

        _mockAnimalUserRepository.Setup(repo => repo.GetAllAnimalUsers())
            .ReturnsAsync(expectedAnimalUsers);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        CollectionAssert.AreEqual(expectedAnimalUsers, result);
        _mockAnimalUserRepository.Verify(repo => repo.GetAllAnimalUsers(), Times.Once);
    }

    [Test]
    public void Handle_ThrowsExceptionOnRepositoryError()
    {
        // Arrange
        var query = new GetAllAnimalUsersQuery();

        _mockAnimalUserRepository.Setup(repo => repo.GetAllAnimalUsers())
            .ThrowsAsync(new Exception("Simulated repository error"));

        // Act & Assert
        Assert.That(async () => await _handler.Handle(query, CancellationToken.None), Throws.Exception.TypeOf<Exception>());
        _mockAnimalUserRepository.Verify(repo => repo.GetAllAnimalUsers(), Times.Once);
    }
}
