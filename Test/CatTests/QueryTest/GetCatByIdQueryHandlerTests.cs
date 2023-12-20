using Application.Queries.Cats;
using Application.Queries.Cats.GetById;
using Domain.Models;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

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
        if (result == null)
        {
            Console.WriteLine("Result is null. Check the handling logic.");
        }

        Assert.NotNull(result);
        Assert.IsInstanceOf<Cat>(result);
        Assert.AreEqual(expectedCat.Id, result.Id);
        Assert.AreEqual(expectedCat.Name, result.Name);

        // Ensure that the repository's GetCatById method was called
        _mockAnimalRepository.Verify(repo => repo.GetCatById(catId), Times.Once);
    }
}
