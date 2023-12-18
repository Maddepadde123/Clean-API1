// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using Domain.Models;              // Importerar namnrymden för domänmodeller
using Infrastructure.Interfaces;  // Importerar namnrymden för gränssnittet för Animal Repository
using MediatR;                    // Importerar namnrymden för MediatR för att stödja Mediator pattern
using System.Threading;           // Importerar namnrymden för trådhanttering
using System.Threading.Tasks;     // Importerar namnrymden för asynkrona uppgifter

// Placerar koden i namnrymden "Application.Commands.Cats.UpdateCat"
namespace Application.Commands.Cats.UpdateCat
{
    // Skapar en klass "UpdateCatByIdCommandHandler" som implementerar IRequestHandler<TRequest, TResponse>
    // där TRequest är typen av förfrågan och TResponse är typen av svar från hanteraren
    public class UpdateCatByIdCommandHandler : IRequestHandler<UpdateCatByIdCommand, Cat>
    {
        // Privat fält för att hålla en referens till Animal Repository-gränssnittet
        private readonly IAnimalRepository _animalRepository;

        // Konstruktor för att skapa en instans av UpdateCatByIdCommandHandler med ett Animal Repository-gränssnitt
        public UpdateCatByIdCommandHandler(IAnimalRepository animalRepository)
        {
            // Tilldelar det inkommande Animal Repository-gränssnittet till det privata fältet
            _animalRepository = animalRepository;
        }

        // Implementerar Handle-metoden för att hantera inkommande UpdateCatByIdCommand-förfrågan
        // Den här metoden används för att utföra logiken för att uppdatera informationen om en katt baserat på dess ID
        public async Task<Cat> Handle(UpdateCatByIdCommand request, CancellationToken cancellationToken)
        {
            // Hämtar den befintliga katten från databasen baserat på det angivna ID:t
            Cat existingCat = await _animalRepository.GetCatById(request.Id);

            // Kontrollerar om katten finns i databasen
            if (existingCat == null)
            {
                // Katten med det angivna ID:t finns inte i databasen
                // Du kan hantera detta på lämpligt sätt, t.ex. kasta ett undantag eller returnera null
                return null;
            }

            // Uppdaterar kattens egenskaper med informationen från den uppdaterade katten
            existingCat.Name = request.UpdatedCat.Name;
            existingCat.LikesToPlay = request.UpdatedCat.LikesToPlay;
            existingCat.CatBreed = request.UpdatedCat.CatBreed;
            existingCat.CatWeight = request.UpdatedCat.CatWeight;

            // Uppdaterar katten i databasen
            await _animalRepository.UpdateCatById(existingCat);

            // Returnerar den uppdaterade katten
            return existingCat;
        }
    }
}
