using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Models;
using Domain.Models.User;
using MediatR;

namespace Application.Commands.Users
{
    public class RegisterUserCommand : IRequest<UserModel>
    {
        public RegisterUserCommand(UserRegistrationDto newUser)
        {
            NewUser = newUser;
        }

        public UserRegistrationDto NewUser { get; }
    }
}