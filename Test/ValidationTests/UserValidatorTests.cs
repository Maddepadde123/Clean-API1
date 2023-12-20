using Application.Dtos;
using Application.Validators.User;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Application.Tests.Validators.User
{
    [TestFixture]
    public class UserValidatorTests
    {
        private UserValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new UserValidator();
        }

        [Test]
        public void Validate_ValidUserDto_ReturnsTrue()
        {
            // Arrange
            var validUserDto = new UserDto
            {
                Username = "john_doe"
            };

            // Act
            var result = _validator.Validate(validUserDto);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Test]
        public void Validate_InvalidUserDto_EmptyUsername_ReturnsFalse()
        {
            // Arrange
            var invalidUserDto = new UserDto
            {
                // Empty Username
            };

            // Act
            var result = _validator.TestValidate(invalidUserDto);

            // Assert
            result.ShouldHaveValidationErrorFor(user => user.Username)
                .WithErrorMessage("Username cant be empty");
        }

        [Test]
        public void Validate_InvalidUserDto_NullUsername_ReturnsFalse()
        {
            // Arrange
            var invalidUserDto = new UserDto
            {
                Username = null // Null Username
            };

            // Act
            var result = _validator.TestValidate(invalidUserDto);

            // Assert
            result.ShouldHaveValidationErrorFor(user => user.Username)
                .WithErrorMessage("Username cant be NULL");
        }
    }
}
