// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using Domain.Models.User;          // Importerar namnrymden för användarmodeller
using Infrastructure.Interfaces;   // Importerar namnrymden för gränssnittet för användarrepository (IUserRepository)
using MediatR;                     // Importerar namnrymden för MediatR för att stödja Mediator pattern

// Placerar koden i namnrymden "Application.Commands.Users.DeleteUser"
namespace Application.Commands.Users.DeleteUser
{
    // Skapar en klass "DeleteUserByIdCommandHandler" som implementerar IRequestHandler<TRequest, TResponse>
    // där TRequest är typen av förfrågan (DeleteUserByIdCommand) och TResponse är typen av svar (bool)
    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, bool>
    {
        // Privat fält för att hålla en referens till användarrepository-gränssnittet (IUserRepository)
        private readonly IUserRepository _userRepository;

        // Konstruktor för att skapa en instans av DeleteUserByIdCommandHandler med ett användarrepository-gränssnitt
        public DeleteUserByIdCommandHandler(IUserRepository userRepository)
        {
            // Injicerar IUserRepository via konstruktorn för att hantera databasinteraktioner med användare
            _userRepository = userRepository;
        }

        // Implementerar Handle-metoden för att hantera inkommande DeleteUserByIdCommand-förfrågan
        // Den här metoden används för att utföra logiken för att ta bort en användare baserat på dess ID
        public async Task<bool> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Hämtar den användare som ska tas bort från databasen baserat på det angivna användar-ID:t
                UserModel userToDelete = await _userRepository.GetUserById(request.DeletedById);

                // Kontrollerar om användaren finns och kan tas bort
                if (userToDelete != null)
                {
                    // Använder IUserRepository för att ta bort användaren från databasen baserat på det angivna ID:t
                    await _userRepository.DeleteUserById(request.DeletedById);

                    // Returnerar true för att indikera att borttagningen var framgångsrik
                    return true;
                }

                // Returnerar false om användaren inte hittades och därmed inte kunde tas bort
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
