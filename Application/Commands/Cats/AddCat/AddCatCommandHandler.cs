// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using Domain.Models;              // Importerar namnrymden för domänmodeller
using Infrastructure.Interfaces;  // Importerar namnrymden för gränssnittet för Animal Repository
using MediatR;                    // Importerar namnrymden för MediatR för att stödja Mediator pattern
using System;                     // Importerar namnrymden för grundläggande systemklasser
using System.Threading;           // Importerar namnrymden för trådhanttering
using System.Threading.Tasks;     // Importerar namnrymden för asynkrona uppgifter

// Placerar koden i namnrymden "Application.Commands.Cats"
namespace Application.Commands.Cats
{
    // Skapar en klass "AddCatCommandHandler" som implementerar IRequestHandler<TRequest, TResponse>
    // där TRequest är typen av förfrågan och TResponse är typen av svar från hanteraren
    public class AddCatCommandHandler : IRequestHandler<AddCatCommand, Cat>
    {
        // Privat fält för att hålla en referens till Animal Repository-gränssnittet
        private readonly IAnimalRepository _animalRepository;

        // Konstruktor för att skapa en instans av AddCatCommandHandler med ett Animal Repository-gränssnitt
        public AddCatCommandHandler(IAnimalRepository animalRepository)
        {
            // Tilldelar det inkommande Animal Repository-gränssnittet till det privata fältet
            _animalRepository = animalRepository;
        }

        // Implementerar Handle-metoden för att hantera inkommande AddCatCommand-förfrågan
        // Den här metoden används för att utföra logiken för att lägga till en ny katt
        public async Task<Cat> Handle(AddCatCommand request, CancellationToken cancellationToken)
        {
            // Skapar en ny Cat-instans med data från AddCatCommand-förfrågan
            Cat catToCreate = new Cat
            {
                Id = Guid.NewGuid(),                    // Skapar ett nytt unikt ID för katten
                Name = request.NewCat.Name,             // Tilldelar kattens namn från förfrågan
                LikesToPlay = request.NewCat.LikesToPlay, // Tilldelar kattens lekpreferens från förfrågan
                CatBreed = request.NewCat.CatBreed,
                CatWeight = request.NewCat.CatWeight,
            };

            // Använder Animal Repository-gränssnittet för att lägga till den nya katten
            await _animalRepository.AddNewCat(catToCreate);

            // Returnerar den nyligen skapade katten
            return catToCreate;
        }
    }
}
