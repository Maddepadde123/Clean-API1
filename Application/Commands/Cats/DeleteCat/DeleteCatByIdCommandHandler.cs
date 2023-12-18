// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using Domain.Models;              // Importerar namnrymden för domänmodeller
using Infrastructure.Interfaces;  // Importerar namnrymden för gränssnittet för Animal Repository
using MediatR;                    // Importerar namnrymden för MediatR för att stödja Mediator pattern
using System;                     // Importerar namnrymden för grundläggande systemklasser
using System.Threading;           // Importerar namnrymden för trådhanttering
using System.Threading.Tasks;     // Importerar namnrymden för asynkrona uppgifter

// Placerar koden i namnrymden "Application.Commands.Cats.DeleteDog"
namespace Application.Commands.Cats.DeleteDog
{
    // Skapar en klass "DeleteCatByIdCommandHandler" som implementerar IRequestHandler<TRequest, TResponse>
    // där TRequest är typen av förfrågan och TResponse är typen av svar från hanteraren
    public class DeleteCatByIdCommandHandler : IRequestHandler<DeleteCatByIdCommand, bool>
    {
        // Privat fält för att hålla en referens till Animal Repository-gränssnittet
        private readonly IAnimalRepository _animalRepository;

        // Konstruktor för att skapa en instans av DeleteCatByIdCommandHandler med ett Animal Repository-gränssnitt
        public DeleteCatByIdCommandHandler(IAnimalRepository animalRepository)
        {
            // Tilldelar det inkommande Animal Repository-gränssnittet till det privata fältet
            _animalRepository = animalRepository;
        }

        // Implementerar Handle-metoden för att hantera inkommande DeleteCatByIdCommand-förfrågan
        // Den här metoden används för att utföra logiken för att ta bort en katt baserat på dess ID
        public async Task<bool> Handle(DeleteCatByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Använder Animal Repository-gränssnittet för att ta bort katten baserat på det angivna ID:t
                await _animalRepository.DeleteCatById(request.DeletedCatId);

                // Returnerar true om borttagningen lyckades
                return true;
            }
            catch (Exception ex)
            {
                // Loggar eller hanterar fel om det uppstår och returnerar false
                // Detta förhindrar att fel i borttagningsprocessen kraschar applikationen
                // Du kan anpassa denna del beroende på ditt loggnings- och felhanteringsbehov
                return false;
            }
        }
    }
}
