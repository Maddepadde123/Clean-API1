// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using Domain.Models;              // Importerar namnrymden för domänmodeller
using Infrastructure.Interfaces;  // Importerar namnrymden för gränssnittet för Animal Repository
using MediatR;                    // Importerar namnrymden för MediatR för att stödja Mediator pattern
using System;                     // Importerar namnrymden för grundläggande systemklasser
using System.Linq;                // Importerar namnrymden för LINQ (Language-Integrated Query)
using System.Threading;           // Importerar namnrymden för trådhanttering
using System.Threading.Tasks;     // Importerar namnrymden för asynkrona uppgifter

// Placerar koden i namnrymden "Application.Commands.Birds.UpdateBird"
namespace Application.Commands.Birds.UpdateBird
{
    // Skapar en klass "UpdateBirdByIdCommandHandler" som implementerar IRequestHandler<TRequest, TResponse>
    // där TRequest är typen av förfrågan och TResponse är typen av svar från hanteraren
    public class UpdateBirdByIdCommandHandler : IRequestHandler<UpdateBirdByIdCommand, Bird>
    {
        // Privat fält för att hålla en referens till Animal Repository-gränssnittet
        private readonly IAnimalRepository _animalRepository;

        // Konstruktor för att skapa en instans av UpdateBirdByIdCommandHandler med ett Animal Repository-gränssnitt
        public UpdateBirdByIdCommandHandler(IAnimalRepository animalRepository)
        {
            // Kastar ett undantag om det inkommande Animal Repository-gränssnittet är null
            _animalRepository = animalRepository ?? throw new ArgumentNullException(nameof(animalRepository));
        }

        // Implementerar Handle-metoden för att hantera inkommande UpdateBirdByIdCommand-förfrågan
        // Den här metoden används för att utföra logiken för att uppdatera informationen om en fågel baserat på dess ID
        public async Task<Bird> Handle(UpdateBirdByIdCommand request, CancellationToken cancellationToken)
        {
            // Hämtar fågeln från Animal Repository baserat på det angivna ID:t
            Bird birdToUpdate = await _animalRepository.GetBirdById(request.Id);

            // Om fågeln hittas, uppdatera dess information och spara den till Animal Repository
            if (birdToUpdate != null)
            {
                birdToUpdate.Name = request.UpdatedBird.Name;
                birdToUpdate.CanFly = request.UpdatedBird.CanFly;
                birdToUpdate.Color = request.UpdatedBird.Color;

                // Använder Animal Repository-gränssnittet för att uppdatera fågeln
                await _animalRepository.UpdateBirdById(birdToUpdate);

                // Returnerar den uppdaterade fågeln
                return birdToUpdate;
            }

            // Om fågeln inte hittas, returnera null eller gör något annat beroende på ditt krav
            return null;
        }
    }
}
