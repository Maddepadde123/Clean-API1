// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using Application.Dtos;   // Importerar namnrymden för Data Transfer Objects (DTOs)
using Domain.Models;      // Importerar namnrymden för domänmodeller (Domain Models)
using MediatR;            // Importerar MediatR-namnrymden för att stödja Mediator pattern

// Placerar koden i namnrymden "Application.Commands.Birds"
namespace Application.Commands.Birds
{
    // Skapar en klass "AddBirdCommand" som är en implementation av IRequest<T>
    // där T är typen av svar som förväntas från MediatR
    public class AddBirdCommand : IRequest<Bird>
    {
        // Konstruktor för att skapa ett nytt AddBirdCommand-objekt med en BirdDto som parameter
        public AddBirdCommand(BirdDto newBird)
        {
            // Tilldelar den inkommande BirdDto till det privata fältet NewBird
            NewBird = newBird;
        }

        // Egenskap (property) för att få åtkomst till den inkommande BirdDto
        public BirdDto NewBird { get; }
    }
}
