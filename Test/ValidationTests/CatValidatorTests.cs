using Application.Dtos;
using Application.Validators;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Application.Tests.Validators
{
    [TestFixture]
    public class CatValidatorTests
    {
        private CatValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CatValidator();
        }

        [Test]
        public void Validate_ValidCatDto_ReturnsTrue()
        {
            // Arrange
            var validCatDto = new CatDto
            {
                Name = "Whiskers",
                LikesToPlay = true,
                CatBreed = "Siamese",
                CatWeight = 8
            };

            // Act
            var result = _validator.Validate(validCatDto);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Test]
        public void Validate_InvalidCatDto_MissingName_ReturnsFalse()
        {
            // Arrange
            var invalidCatDto = new CatDto
            {
                // Missing Name
                LikesToPlay = false,
                CatBreed = "Persian",
                CatWeight = 5
            };

            // Act
            var result = _validator.TestValidate(invalidCatDto);

            // Assert
            result.ShouldHaveValidationErrorFor(dto => dto.Name)
                .WithErrorMessage("Name is required.");
        }

        [Test]
        public void Validate_InvalidCatDto_NameExceedsMaxLength_ReturnsFalse()
        {
            // Arrange
            var invalidCatDto = new CatDto
            {
                Name = new string('A', 51), // Exceeds maximum length
                LikesToPlay = true,
                CatBreed = "Maine Coon",
                CatWeight = 12
            };

            // Act
            var result = _validator.TestValidate(invalidCatDto);

            // Assert
            result.ShouldHaveValidationErrorFor(dto => dto.Name)
                .WithErrorMessage("Name cannot exceed 50 characters.");
        }

        [Test]
        public void Validate_InvalidCatDto_MissingLikesToPlay_ReturnsFalse()
        {
            // Arrange
            var invalidCatDto = new CatDto
            {
                Name = "Fluffy",
                // Missing LikesToPlay
                CatBreed = "Ragdoll",
                CatWeight = 10
            };

            // Act
            var result = _validator.TestValidate(invalidCatDto);

            // Assert
            result.ShouldHaveValidationErrorFor(dto => dto.LikesToPlay)
                .WithErrorMessage("LikesToPlay is required.");
        }

        [Test]
        public void Validate_InvalidCatDto_CatBreedExceedsMaxLength_ReturnsFalse()
        {
            // Arrange
            var invalidCatDto = new CatDto
            {
                Name = "Mittens",
                LikesToPlay = false,
                CatBreed = new string('B', 51), // Exceeds maximum length
                CatWeight = 7
            };

            // Act
            var result = _validator.TestValidate(invalidCatDto);

            // Assert
            result.ShouldHaveValidationErrorFor(dto => dto.CatBreed)
                .WithErrorMessage("Breed cannot exceed 50 characters.");
        }

        [Test]
        public void Validate_InvalidCatDto_CatWeightOutOfRange_ReturnsFalse()
        {
            // Arrange
            var invalidCatDto = new CatDto
            {
                Name = "Leo",
                LikesToPlay = true,
                CatBreed = "Bengal",
                CatWeight = 120 // Out of range
            };

            // Act
            var result = _validator.TestValidate(invalidCatDto);

            // Assert
            result.ShouldHaveValidationErrorFor(dto => dto.CatWeight)
                .WithErrorMessage("Weight must be between 1 and 100.");
        }
    }
}
