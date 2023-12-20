using Application.Dtos;
using Application.Validators;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Application.Tests.Validators
{
    [TestFixture]
    public class BirdValidatorTests
    {
        private BirdValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new BirdValidator();
        }

        [Test]
        public void Validate_ValidBirdDto_ReturnsTrue()
        {
            // Arrange
            var validBirdDto = new BirdDto
            {
                Name = "Sparrow",
                CanFly = true,
                Color = "Brown"
            };

            // Act
            var result = _validator.Validate(validBirdDto);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Test]
        public void Validate_InvalidBirdDto_MissingName_ReturnsFalse()
        {
            // Arrange
            var invalidBirdDto = new BirdDto
            {
                // Missing Name
                CanFly = true,
                Color = "Blue"
            };

            // Act
            var result = _validator.TestValidate(invalidBirdDto);

            // Assert
            result.ShouldHaveValidationErrorFor(dto => dto.Name)
                .WithErrorMessage("Name is required.");
        }

        [Test]
        public void Validate_InvalidBirdDto_NameExceedsMaxLength_ReturnsFalse()
        {
            // Arrange
            var invalidBirdDto = new BirdDto
            {
                Name = "NameThatExceedsMaxLength", // Exceeds maximum length
                CanFly = true,
                Color = "Green"
            };

            // Act
            var result = _validator.TestValidate(invalidBirdDto);

            // Assert
            result.ShouldHaveValidationErrorFor(dto => dto.Name)
                .WithErrorMessage("Name cannot exceed 50 characters.");
        }

        [Test]
        public void Validate_InvalidBirdDto_MissingCanFly_ReturnsFalse()
        {
            // Arrange
            var invalidBirdDto = new BirdDto
            {
                Name = "Parrot",
                // Missing CanFly
                Color = "Red"
            };

            // Act
            var result = _validator.TestValidate(invalidBirdDto);

            // Assert
            result.ShouldHaveValidationErrorFor(dto => dto.CanFly)
                .WithErrorMessage("CanFly is required.");
        }

        [Test]
        public void Validate_InvalidBirdDto_MissingColor_ReturnsFalse()
        {
            // Arrange
            var invalidBirdDto = new BirdDto
            {
                Name = "Crow",
                CanFly = false,
                // Missing Color
            };

            // Act
            var result = _validator.TestValidate(invalidBirdDto);

            // Assert
            result.ShouldHaveValidationErrorFor(dto => dto.Color)
                .WithErrorMessage("Color is required.");
        }
    }
}
