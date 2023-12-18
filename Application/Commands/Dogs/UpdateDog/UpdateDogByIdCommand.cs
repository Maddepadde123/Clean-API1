// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using Application.Dtos;  // Importerar namnrymden för Data Transfer Objects (DTOs)
using Domain.Models;      // Importerar namnrymden för domänmodeller, inklusive Dog-klassen
using MediatR;            // Importerar namnrymden för MediatR för att stödja Mediator pattern

// Placerar koden i namnrymden "Application.Commands.Dogs.UpdateDog"
namespace Application.Commands.Dogs.UpdateDog
{
    // Skapar en klass "UpdateDogByIdCommand" som är en implementation av IRequest<T>
    // där T är typen av svar som förväntas från MediatR, i det här fallet en Dog
    public class UpdateDogByIdCommand : IRequest<Dog>
    {
        // Konstruktor för att skapa ett nytt UpdateDogByIdCommand-objekt med en DogDto och ett ID som parametrar
        public UpdateDogByIdCommand(DogDto updatedDog, Guid id)
        {
            // Tilldelar den inkommande DogDto och ID till de publika egenskaperna UpdatedDog och Id
            UpdatedDog = updatedDog;
            Id = id;
        }

        // Publik egenskap (property) för att få åtkomst till den uppdaterade hunden (DogDto)
        public DogDto UpdatedDog { get; }

        // Publik egenskap (property) för att få åtkomst till det inkommande ID:t
        public Guid Id { get; }
    }
}
