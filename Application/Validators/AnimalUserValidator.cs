using Application.Dtos;
using FluentValidation;

namespace Application.Validators
{
    public class AnimalUserValidator : AbstractValidator<AnimalUserDto>
    {
        public AnimalUserValidator()
        {
            RuleFor(dto => dto.UserId)
           .NotEmpty().WithMessage("UserId is required.");

            RuleFor(dto => dto.AnimalId)
                .NotEmpty().WithMessage("AnimalId is required.");
        }

       
    }
}