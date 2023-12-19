using Application.Dtos;
using FluentValidation;

namespace Application.Validators
{
    public class BirdValidator : AbstractValidator<BirdDto>
    {
        public BirdValidator()
        {
            RuleFor(dto => dto.Name)
           .NotEmpty().WithMessage("Name is required.")
           .MaximumLength(12).WithMessage("Name cannot exceed 50 characters.");

            RuleFor(dto => dto.CanFly)
                .NotEmpty().WithMessage("CanFly is required.");

            RuleFor(dto => dto.Color)
                .NotEmpty().WithMessage("Color is required.")
                .MaximumLength(50).WithMessage("Color cannot exceed 50 characters.");
        }
    }



}