// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using Domain.Models;              // Importerar namnrymden för domänmodeller
using Infrastructure.Interfaces;  // Importerar namnrymden för gränssnittet för Animal Repository
using MediatR;                    // Importerar namnrymden för MediatR för att stödja Mediator pattern
using System.Threading;           // Importerar namnrymden för trådhanttering
using System.Threading.Tasks;     // Importerar namnrymden för asynkrona uppgifter

// Placerar koden i namnrymden "Application.Commands.Birds.DeleteDog"
namespace Application.Commands.Birds.DeleteDog
{
    // Skapar en klass "DeleteBirdByIdCommandHandler" som implementerar IRequestHandler<TRequest, TResponse>
    // där TRequest är typen av förfrågan och TResponse är typen av svar från hanteraren
    public class DeleteBirdByIdCommandHandler : IRequestHandler<DeleteBirdByIdCommand, bool>
    {
        // Privat fält för att hålla en referens till Animal Repository-gränssnittet
        private readonly IAnimalRepository _animalRepository;

        // Konstruktor för att skapa en instans av DeleteBirdByIdCommandHandler med ett Animal Repository-gränssnitt
        public DeleteBirdByIdCommandHandler(IAnimalRepository animalRepository)
        {
            // Tilldelar det inkommande Animal Repository-gränssnittet till det privata fältet
            _animalRepository = animalRepository;
        }

        // Implementerar Handle-metoden för att hantera inkommande DeleteBirdByIdCommand-förfrågan
        // Den här metoden används för att utföra logiken för att ta bort en fågel baserat på dess ID
        public async Task<bool> Handle(DeleteBirdByIdCommand request, CancellationToken cancellationToken)
        {
            // Använder Animal Repository-gränssnittet för att ta bort fågeln baserat på det angivna ID
            await _animalRepository.DeleteBirdById(request.DeletedBirdId);

            // Returnera true som standard, men du kan lägga till logik för att kontrollera om borttagningen var framgångsrik
            return true;
        }
    }
}
