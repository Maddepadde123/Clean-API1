// Importerar nödvändiga namnrymder och klasser för att använda Dog-klassen, IAnimalRepository, MediatR, CancellationToken och Task
using Domain.Models;               // Importerar namnrymden för domänmodeller, inklusive Dog-klassen
using Infrastructure.Interfaces;   // Importerar namnrymden för gränssnittet för Animal Repository (IAnimalRepository)
using MediatR;                     // Importerar namnrymden för MediatR för att stödja Mediator pattern
using System.Threading;            // Importerar namnrymden för trådhanttering (CancellationToken)
using System.Threading.Tasks;      // Importerar namnrymden för asynkrona uppgifter (Task)

// Placerar koden i namnrymden "Application.Commands.Dogs"
namespace Application.Commands.Dogs
{
    // Skapar en klass "AddDogCommandHandler" som implementerar IRequestHandler<TRequest, TResponse>
    // där TRequest är typen av förfrågan (AddDogCommand) och TResponse är typen av svar (Dog)
    public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        // Privat fält för att hålla en referens till Animal Repository-gränssnittet (IAnimalRepository)
        private readonly IAnimalRepository _animalRepository;

        // Konstruktor för att skapa en instans av AddDogCommandHandler med ett Animal Repository-gränssnitt
        public AddDogCommandHandler(IAnimalRepository animalRepository)
        {
            // Injicerar IAnimalRepository via konstruktorn för att hantera databasinteraktioner
            _animalRepository = animalRepository;
        }

        // Implementerar Handle-metoden för att hantera inkommande AddDogCommand-förfrågan
        // Den här metoden används för att utföra logiken för att lägga till en ny hund i databasen
        public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            // Skapar en ny Dog-instans med automatisk egenskapsinitiering
            Dog dogToCreate = new()
            {
                // Genererar ett nytt unikt ID för hunden
                Id = Guid.NewGuid(),

                // Använder namnet från kommandot för att sätta hundens namn
                Name = request.NewDog.Name,
                DogBreed = request.NewDog.DogBreed,
                DogWeight = request.NewDog.DogWeight,
            };

            // Använder IAnimalRepository för att lägga till den nya hunden i databasen
            await _animalRepository.AddNewDog(dogToCreate);

            // Returnerar den nyskapade hunden som svar på förfrågan
            return dogToCreate;
        }
    }
}
