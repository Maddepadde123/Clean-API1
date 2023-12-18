using Domain.Models.User;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Users.UpdateUser
{
    public class UpdateUserByIdCommandHandler : IRequestHandler<UpdateUserByIdCommand, UserModel>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserByIdCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> Handle(UpdateUserByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Hämta den befintliga användaren från databasen baserat på ID
                UserModel existingUser = await _userRepository.GetUserById(request.UserId);

                if (existingUser == null)
                {
                    // Hantera scenariot där användaren inte finns
                    throw new InvalidOperationException($"User with ID {request.UserId} not found.");
                }

                // Uppdatera användarens egenskaper baserat på kommandot
                existingUser.UserName = request.UpdatedUser.Username;
                // Lägg till andra uppdaterade egenskaper här efter behov

                // Använd UserRepository för att uppdatera användaren i databasen
                await _userRepository.UpdateUserById(existingUser);

                return existingUser;
            }
            catch (Exception ex)
            {
                // Logga och hantera fel här
                throw;
            }
        }
    }
}
