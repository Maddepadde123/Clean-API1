using Application.Dtos;
using FluentValidation;

namespace Application.Validators
{
    public class CatValidator : AbstractValidator<CatDto>
    {
        public CatValidator()
        {
            RuleFor(dto => dto.Name)
           .NotEmpty().WithMessage("Name is required.")
           .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");

            RuleFor(dto => dto.LikesToPlay)
                .NotEmpty().WithMessage("LikesToPlay is required.");

            RuleFor(dto => dto.CatBreed)
                .MaximumLength(50).WithMessage("Breed cannot exceed 50 characters.");

            RuleFor(dto => dto.CatWeight)
                .NotNull().WithMessage("Weight is required.")
                .InclusiveBetween(1, 100).WithMessage("Weight must be between 1 and 100.");
        }

    }
}