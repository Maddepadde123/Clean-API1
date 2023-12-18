// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using Domain.Models;              // Importerar namnrymden för domänmodeller
using Infrastructure.Interfaces;  // Importerar namnrymden för gränssnittet för Animal Repository
using MediatR;                   // Importerar namnrymden för MediatR för att stödja Mediator pattern

// Placerar koden i namnrymden "Application.Commands.Birds"
namespace Application.Commands.Birds
{
    // Skapar en klass "AddBirdCommandHandler" som implementerar IRequestHandler<TRequest, TResponse>
    // där TRequest är typen av förfrågan och TResponse är typen av svar från hanteraren
    public class AddBirdCommandHandler : IRequestHandler<AddBirdCommand, Bird>
    {
        // Privat fält för att hålla en referens till Animal Repository-gränssnittet
        private readonly IAnimalRepository _animalRepository;

        // Konstruktor för att skapa en instans av AddBirdCommandHandler med ett Animal Repository-gränssnitt
        public AddBirdCommandHandler(IAnimalRepository animalRepository)
        {
            // Tilldelar det inkommande Animal Repository-gränssnittet till det privata fältet
            _animalRepository = animalRepository;
        }

        // Implementerar Handle-metoden för att hantera inkommande AddBirdCommand-förfrågan
        // Den här metoden skapas för att hantera logiken för att lägga till en ny fågel
        public async Task<Bird> Handle(AddBirdCommand request, CancellationToken cancellationToken)
        {
            // Skapar en ny Bird-instans med data från AddBirdCommand-förfrågan
            Bird birdToCreate = new()
            {
                Id = Guid.NewGuid(),                // Skapar ett nytt unikt ID för fågeln
                Name = request.NewBird.Name,       // Tilldelar fågelns namn från förfrågan
                CanFly = request.NewBird.CanFly,    // Tilldelar fågelns flygförmåga från förfrågan
                Color = request.NewBird.Color,
            };

            // Använder Animal Repository-gränssnittet för att lägga till den nya fågeln
            await _animalRepository.AddNewBird(birdToCreate);

            // Returnerar den nyligen skapade fågeln
            return birdToCreate;
        }
    }
}
