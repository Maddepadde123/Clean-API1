// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using Application.Dtos;  // Importerar namnrymden för Data Transfer Objects (DTOs)
using Domain.Models;      // Importerar namnrymden för domänmodeller
using MediatR;            // Importerar namnrymden för MediatR för att stödja Mediator pattern

// Placerar koden i namnrymden "Application.Commands.Cats.UpdateCat"
namespace Application.Commands.Cats.UpdateCat
{
    // Skapar en klass "UpdateCatByIdCommand" som är en implementation av IRequest<T>
    // där T är typen av svar som förväntas från MediatR, i det här fallet en Cat
    public class UpdateCatByIdCommand : IRequest<Cat>
    {
        // Konstruktor för att skapa ett nytt UpdateCatByIdCommand-objekt med en CatDto och ett ID som parametrar
        public UpdateCatByIdCommand(CatDto updatedCat, Guid id)
        {
            // Tilldelar den inkommande CatDto och ID till de publika egenskaperna UpdatedCat och Id
            UpdatedCat = updatedCat;
            Id = id;
        }

        // Publik egenskap (property) för att få åtkomst till den inkommande CatDto
        public CatDto UpdatedCat { get; }

        // Publik egenskap (property) för att få åtkomst till det inkommande ID:t
        public Guid Id { get; }
    }
}
