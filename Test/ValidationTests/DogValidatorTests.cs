using Application.Dtos;
using Application.Validators;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Application.Tests.Validators
{
    [TestFixture]
    public class DogValidatorTests
    {
        private DogValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new DogValidator();
        }

        [Test]
        public void Validate_ValidDogDto_ReturnsTrue()
        {
            // Arrange
            var validDogDto = new DogDto
            {
                Name = "Buddy",
                DogBreed = "Golden Retriever",
                DogWeight = 25
            };

            // Act
            var result = _validator.Validate(validDogDto);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Test]
        public void Validate_InvalidDogDto_MissingName_ReturnsFalse()
        {
            // Arrange
            var invalidDogDto = new DogDto
            {
                // Missing Name
                DogBreed = "Labrador",
                DogWeight = 30
            };

            // Act
            var result = _validator.TestValidate(invalidDogDto);

            // Assert
            result.ShouldHaveValidationErrorFor(dto => dto.Name)
                .WithErrorMessage("Name is required.");
        }

        [Test]
        public void Validate_InvalidDogDto_NameExceedsMaxLength_ReturnsFalse()
        {
            // Arrange
            var invalidDogDto = new DogDto
            {
                Name = new string('A', 51), // Exceeds maximum length
                DogBreed = "Poodle",
                DogWeight = 15
            };

            // Act
            var result = _validator.TestValidate(invalidDogDto);

            // Assert
            result.ShouldHaveValidationErrorFor(dto => dto.Name)
                .WithErrorMessage("Name cannot exceed 50 characters.");
        }

        [Test]
        public void Validate_InvalidDogDto_DogBreedExceedsMaxLength_ReturnsFalse()
        {
            // Arrange
            var invalidDogDto = new DogDto
            {
                Name = "Max",
                DogBreed = new string('B', 51), // Exceeds maximum length
                DogWeight = 20
            };

            // Act
            var result = _validator.TestValidate(invalidDogDto);

            // Assert
            result.ShouldHaveValidationErrorFor(dto => dto.DogBreed)
                .WithErrorMessage("Breed cannot exceed 50 characters.");
        }

        [Test]
        public void Validate_InvalidDogDto_DogWeightOutOfRange_ReturnsFalse()
        {
            // Arrange
            var invalidDogDto = new DogDto
            {
                Name = "Charlie",
                DogBreed = "Dachshund",
                DogWeight = 120 // Out of range
            };

            // Act
            var result = _validator.TestValidate(invalidDogDto);

            // Assert
            result.ShouldHaveValidationErrorFor(dto => dto.DogWeight)
                .WithErrorMessage("Weight must be between 1 and 100.");
        }
    }
}
