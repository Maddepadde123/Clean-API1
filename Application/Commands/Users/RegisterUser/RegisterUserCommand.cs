using Application.Dtos;
using Domain.Models.User;
using MediatR;

namespace Application.Commands.Users.RegisterUser
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
