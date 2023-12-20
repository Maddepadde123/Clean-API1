using Application.Validators;
using FluentAssertions;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace Application.Tests.Validators
{
    [TestFixture]
    public class GuidValidatorTests
    {
        private GuidValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new GuidValidator();
        }

        [Test]
        public void Validate_ValidGuid_ReturnsTrue()
        {
            // Arrange
            var validGuid = Guid.NewGuid();

            // Act
            var result = _validator.Validate(validGuid);

            // Assert
            result.IsValid.Should().BeTrue();
        }

        [Test]
        public void Validate_InvalidGuid_EmptyGuid_ReturnsFalse()
        {
            // Arrange
            var emptyGuid = Guid.Empty;

            // Act
            var result = _validator.TestValidate(emptyGuid);

            // Assert
            result.ShouldHaveValidationErrorFor(guid => guid)
                .WithErrorMessage("Guid cant be empty");
        }
    }
}
