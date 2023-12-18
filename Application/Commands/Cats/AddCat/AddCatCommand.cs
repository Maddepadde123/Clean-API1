// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using Application.Dtos;   // Importerar namnrymden för Data Transfer Objects (DTOs)
using Domain.Models;      // Importerar namnrymden för domänmodeller
using MediatR;            // Importerar namnrymden för MediatR för att stödja Mediator pattern

// Placerar koden i namnrymden "Application.Commands.Cats"
namespace Application.Commands.Cats
{
    // Skapar en klass "AddCatCommand" som är en implementation av IRequest<T>
    // där T är typen av svar som förväntas från MediatR, i det här fallet en Cat
    public class AddCatCommand : IRequest<Cat>
    {
        // Konstruktor för att skapa ett nytt AddCatCommand-objekt med en CatDto som parameter
        public AddCatCommand(CatDto newCat)
        {
            // Tilldelar den inkommande CatDto till det privata fältet NewCat
            NewCat = newCat;
        }

        // Publik egenskap (property) för att få åtkomst till den inkommande CatDto
        public CatDto NewCat { get; }
    }
}
