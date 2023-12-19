using Application.Dtos;
using FluentValidation;

namespace Application.Validators
{
    public class DogValidator : AbstractValidator<DogDto>
    {
        public DogValidator()
        {
            RuleFor(dto => dto.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

            RuleFor(dto => dto.DogBreed)
                .MaximumLength(50).WithMessage("Breed cannot exceed 50 characters.");

            RuleFor(dto => dto.DogWeight)
                .NotNull().WithMessage("Weight is required.")
                .InclusiveBetween(1, 100).WithMessage("Weight must be between 1 and 100.");
        }

    }
}