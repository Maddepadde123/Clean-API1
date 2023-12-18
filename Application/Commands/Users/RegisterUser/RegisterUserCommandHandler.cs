using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models.User;
using Infrastructure.Interfaces;

namespace Application.Commands.Users.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserModel>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validate the request (e.g., check for required fields)

                // Check if the username is unique in the real database
                //var isUsernameUnique = await _userRepository.IsUsernameUnique(request.NewUser.Username);
                //if (!isUsernameUnique)
                //{
                //    throw new InvalidOperationException("Username is already taken");
                //}

                // Hash the password before storing it (you need to implement PasswordHasher)
                // var hashedPassword = PasswordHasher.HashPassword(request.NewUser.Password);

                // Create a new user entity with the registration data
                var newUser = new UserModel
                {
                    Id = Guid.NewGuid(),
                    UserName = request.NewUser.Username,
                    UserPassword = request.NewUser.Password,
                    // Include other user-related properties as needed
                };

                // Save the new user to the repository
                await _userRepository.RegisterUser(newUser);

                return newUser;
            }
            catch (Exception ex)
            {
                // Logga och hantera fel här
                throw;
            }
        }
    }
}
