﻿using Application.Dtos;
using FluentValidation;

namespace Application.Validators.User
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(user => user.Username).NotEmpty().WithMessage("Username cant be empty")
                .NotNull().WithMessage("Username cant be NULL");

        }
    }
}