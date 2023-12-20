using Application.Dtos;
using Application.Validators;
using NUnit.Framework;

namespace Application.Tests.Validators
{
    [TestFixture]
    public class AnimalUserValidatorTests
    {
        private AnimalUserValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new AnimalUserValidator();
        }

        [Test]
        public void Validate_ValidAnimalUserDto_ReturnsTrue()
        {
            // Arrange
            var validAnimalUserDto = new AnimalUserDto
            {
                UserId = Guid.NewGuid(),
                AnimalId = Guid.NewGuid(),
            };

            // Act
            var result = _validator.Validate(validAnimalUserDto);

            // Assert
            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void Validate_InvalidAnimalUserDto_MissingUserId_ReturnsFalse()
        {
            // Arrange
            var invalidAnimalUserDto = new AnimalUserDto
            {
                // Missing UserId
                AnimalId = Guid.NewGuid(),
            };

            // Act
            var result = _validator.Validate(invalidAnimalUserDto);

            // Assert
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void Validate_InvalidAnimalUserDto_MissingAnimalId_ReturnsFalse()
        {
            // Arrange
            var invalidAnimalUserDto = new AnimalUserDto
            {
                UserId = Guid.NewGuid(),
                // Missing AnimalId
            };

            // Act
            var result = _validator.Validate(invalidAnimalUserDto);

            // Assert
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void Validate_InvalidAnimalUserDto_MissingBothUserIdAndAnimalId_ReturnsFalse()
        {
            // Arrange
            var invalidAnimalUserDto = new AnimalUserDto
            {
                // Missing both UserId and AnimalId
            };

            // Act
            var result = _validator.Validate(invalidAnimalUserDto);

            // Assert
            Assert.IsFalse(result.IsValid);
        }
    }
}
