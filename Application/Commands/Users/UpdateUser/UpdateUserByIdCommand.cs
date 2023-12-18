﻿using Application.Dtos;
using Domain.Models.User;
using MediatR;
using System;

namespace Application.Commands.Users.UpdateUser
{
    public class UpdateUserByIdCommand : IRequest<UserModel>
    {
        public UpdateUserByIdCommand(UserDto updatedUser, Guid userId)
        {
            UpdatedUser = updatedUser;
            UserId = userId;
        }

        public UserDto UpdatedUser { get; }
        public Guid UserId { get; }
    }
}
