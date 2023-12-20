﻿using Application.Queries.Dogs;
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Interfaces;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Queries.Dogs
{
    [TestFixture]
    public class GetAllDogsQueryHandlerTests
    {
        private GetAllDogsQueryHandler _handler;
        private Mock<IAnimalRepository> _mockAnimalRepository;

        [SetUp]
        public void Setup()
        {
            _mockAnimalRepository = new Mock<IAnimalRepository>();
            _handler = new GetAllDogsQueryHandler(_mockAnimalRepository.Object);
        }

        [Test]
        public async Task Handle_ReturnsAllDogsFromRepository()
        {
            // Arrange
            var expectedDogs = new List<Dog>
            {
                new Dog { Id = Guid.NewGuid(), Name = "Dog1", DogBreed = "Labrador" },
                new Dog { Id = Guid.NewGuid(), Name = "Dog2", DogWeight = 21 }
            };

            _mockAnimalRepository.Setup(repo => repo.GetAllDogs()).ReturnsAsync(expectedDogs);

            // Act
            var result = await _handler.Handle(new GetAllDogsQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<List<Dog>>(result);
            Assert.AreEqual(expectedDogs.Count, result.Count);
        }
    }
}