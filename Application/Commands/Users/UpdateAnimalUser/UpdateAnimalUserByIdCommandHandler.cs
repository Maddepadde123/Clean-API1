using Domain.Models.AnimalUser;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Users.UpdateAnimalUser
{
    public class UpdateAnimalUserByIdCommandHandler : IRequestHandler<UpdateAnimalUserByIdCommand, AnimalUserModel>
    {
        private readonly IAnimalUserRepository _animalUserRepository;

        public UpdateAnimalUserByIdCommandHandler(IAnimalUserRepository animalUserRepository)
        {
            _animalUserRepository = animalUserRepository;
        }

        public async Task<AnimalUserModel> Handle(UpdateAnimalUserByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Hämta den befintliga djur-användaren från databasen baserat på ID
                AnimalUserModel existingAnimalUser = await _animalUserRepository.GetAnimalUserById(request.UserId, request.AnimalId);

                if (existingAnimalUser == null)
                {
                    // Hantera scenariot där djur-användaren inte finns
                    throw new InvalidOperationException($"AnimalUser with User ID {request.UserId} and Animal ID {request.AnimalId} not found.");
                }

                // Använd AnimalUserRepository för att uppdatera djur-användaren i databasen
                await _animalUserRepository.UpdateAnimalUser(existingAnimalUser);

                return existingAnimalUser;
            }
            catch (Exception ex)
            {
                // Logga och hantera fel här
                throw;
            }
        }
    }
}
