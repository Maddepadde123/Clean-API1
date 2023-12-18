// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using Domain.Models.AnimalUser;            // Importerar namnrymden för djur-användarmodeller
using Infrastructure.Interfaces;           // Importerar namnrymden för gränssnittet för djur-användarrepository (IAnimalUserRepository)
using MediatR;                             // Importerar namnrymden för MediatR för att stödja Mediator pattern

// Placerar koden i namnrymden "Application.Commands.Users.DeleteAnimalUser"
namespace Application.Commands.Users.DeleteAnimalUser
{
    // Skapar en klass "DeleteAnimalUserByIdCommandHandler" som implementerar IRequestHandler<TRequest, TResponse>
    // där TRequest är typen av förfrågan (DeleteAnimalUserByIdCommand) och TResponse är typen av svar (bool)
    public class DeleteAnimalUserByIdCommandHandler : IRequestHandler<DeleteAnimalUserByIdCommand, bool>
    {
        // Privat fält för att hålla en referens till djur-användarrepository-gränssnittet (IAnimalUserRepository)
        private readonly IAnimalUserRepository _animalUserRepository;

        // Konstruktor för att skapa en instans av DeleteAnimalUserByIdCommandHandler med ett djur-användarrepository-gränssnitt
        public DeleteAnimalUserByIdCommandHandler(IAnimalUserRepository animalUserRepository)
        {
            // Injicerar IAnimalUserRepository via konstruktorn för att hantera databasinteraktioner med djur-användare
            _animalUserRepository = animalUserRepository;
        }

        // Implementerar Handle-metoden för att hantera inkommande DeleteAnimalUserByIdCommand-förfrågan
        // Den här metoden används för att utföra logiken för att ta bort en djur-användare baserat på dess ID
        public async Task<bool> Handle(DeleteAnimalUserByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Hämtar den djur-användare som ska tas bort från databasen baserat på de angivna ID:t
                AnimalUserModel animalUserToDelete = await _animalUserRepository.GetAnimalUserById(request.UserId, request.AnimalId);

                // Kontrollerar om djur-användaren finns och kan tas bort
                if (animalUserToDelete != null)
                {
                    // Använder IAnimalUserRepository för att ta bort djur-användaren från databasen baserat på de angivna ID:t
                    await _animalUserRepository.DeleteAnimalUserById(request.UserId, request.AnimalId);

                    // Returnerar true för att indikera att borttagningen var framgångsrik
                    return true;
                }

                // Returnerar false om djur-användaren inte hittades och därmed inte kunde tas bort
                return false;
            }
            catch (Exception ex)
            {
                // Loggar och hanterar eventuella fel som uppstår under borttagningsprocessen
                // Du kan anpassa detta beroende på ditt loggnings- och felhanteringsbehov
                return false;
            }
        }
    }
}
