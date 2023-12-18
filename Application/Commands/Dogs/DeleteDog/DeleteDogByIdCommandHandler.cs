// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using Domain.Models;               // Importerar namnrymden för domänmodeller, inklusive Dog-klassen
using Infrastructure.Interfaces;   // Importerar namnrymden för gränssnittet för Animal Repository (IAnimalRepository)
using MediatR;                     // Importerar namnrymden för MediatR för att stödja Mediator pattern
using System;                      // Importerar namnrymden för grundläggande systemklasser
using System.Threading;            // Importerar namnrymden för trådhanttering
using System.Threading.Tasks;      // Importerar namnrymden för asynkrona uppgifter

// Placerar koden i namnrymden "Application.Commands.Dogs.DeleteDog"
namespace Application.Commands.Dogs.DeleteDog
{
    // Skapar en klass "DeleteDogByIdCommandHandler" som implementerar IRequestHandler<TRequest, TResponse>
    // där TRequest är typen av förfrågan (DeleteDogByIdCommand) och TResponse är typen av svar (bool)
    public class DeleteDogByIdCommandHandler : IRequestHandler<DeleteDogByIdCommand, bool>
    {
        // Privat fält för att hålla en referens till Animal Repository-gränssnittet (IAnimalRepository)
        private readonly IAnimalRepository _animalRepository;

        // Konstruktor för att skapa en instans av DeleteDogByIdCommandHandler med ett Animal Repository-gränssnitt
        public DeleteDogByIdCommandHandler(IAnimalRepository animalRepository)
        {
            // Injicerar IAnimalRepository via konstruktorn för att hantera databasinteraktioner
            _animalRepository = animalRepository;
        }

        // Implementerar Handle-metoden för att hantera inkommande DeleteDogByIdCommand-förfrågan
        // Den här metoden används för att utföra logiken för att ta bort en hund baserat på dess ID
        public async Task<bool> Handle(DeleteDogByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Använder AnimalRepository för att ta bort hunden från databasen baserat på det angivna ID:t
                await _animalRepository.DeleteDogById(request.DeletedDogId);

                // Returnerar true om borttagningen lyckades
                return true;
            }
            catch (Exception ex)
            {
                // Loggar och hanterar fel om det uppstår under borttagningsprocessen
                // Du kan anpassa detta beroende på ditt loggnings- och felhanteringsbehov
                return false;
            }
        }
    }
}
