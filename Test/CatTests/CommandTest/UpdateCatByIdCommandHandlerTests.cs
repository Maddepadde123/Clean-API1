using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Domain.Models;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

[TestFixture]
public class UpdateCatByIdCommandHandlerTests
{
    private UpdateCatByIdCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        var mockAnimalRepository = new Mock<IAnimalRepository>();
        _handler = new UpdateCatByIdCommandHandler(mockAnimalRepository.Object);
    }

    [Test]
    public async Task Handle_UpdateCatInDatabase()
    {
        // Arrange
        var catId = Guid.NewGuid();
        var updatedCatDto = new CatDto
        {
            Name = "UpdatedCatName",
            LikesToPlay = true,
            CatBreed = "Persian",
            CatWeight = 5
        };
        var command = new UpdateCatByIdCommand(updatedCatDto, catId);

        var existingCat = new Cat
        {
            Id = catId,
            Name = "OriginalCatName",
            LikesToPlay = false,
            CatBreed = "Siamese",
            CatWeight = 4
        };

        var animalRepositoryMock = new Mock<IAnimalRepository>();
        animalRepositoryMock.Setup(repo => repo.GetCatById(It.IsAny<Guid>())).ReturnsAsync(existingCat);

        _handler = new UpdateCatByIdCommandHandler(animalRepositoryMock.Object);

        // Act
        var updatedCat = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(updatedCat);
        Assert.IsInstanceOf<Cat>(updatedCat);
        Assert.AreEqual(updatedCatDto.Name, updatedCat.Name);
        Assert.AreEqual(updatedCatDto.LikesToPlay, updatedCat.LikesToPlay);
        Assert.AreEqual(updatedCatDto.CatBreed, updatedCat.CatBreed);
        Assert.AreEqual(updatedCatDto.CatWeight, updatedCat.CatWeight, 0.001);

        // Ensure that the repository's UpdateCatById method was called with the correct arguments
        animalRepositoryMock.Verify(repo => repo.UpdateCatById(It.IsAny<Cat>()), Times.Once);
    }
}
