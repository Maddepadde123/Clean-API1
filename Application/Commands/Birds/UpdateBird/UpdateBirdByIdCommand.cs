// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using Application.Dtos;  // Importerar namnrymden för Data Transfer Objects (DTOs)
using Domain.Models;      // Importerar namnrymden för domänmodeller
using MediatR;            // Importerar namnrymden för MediatR för att stödja Mediator pattern

// Placerar koden i namnrymden "Application.Commands.Birds.UpdateBird"
namespace Application.Commands.Birds.UpdateBird
{
    // Skapar en klass "UpdateBirdByIdCommand" som är en implementation av IRequest<T>
    // där T är typen av svar som förväntas från MediatR, i det här fallet en Bird
    public class UpdateBirdByIdCommand : IRequest<Bird>
    {
        // Konstruktor för att skapa ett nytt UpdateBirdByIdCommand-objekt med en BirdDto och ett ID som parametrar
        public UpdateBirdByIdCommand(BirdDto updatedBird, Guid id)
        {
            // Tilldelar den inkommande BirdDto och ID till de publika egenskaperna UpdatedBird och Id
            UpdatedBird = updatedBird;
            Id = id;
        }

        // Publik egenskap (property) för att få åtkomst till den inkommande BirdDto
        public BirdDto UpdatedBird { get; }

        // Publik egenskap (property) för att få åtkomst till det inkommande ID:t
        public Guid Id { get; }
    }
}
