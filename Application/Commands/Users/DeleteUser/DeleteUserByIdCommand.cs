// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using System;          // Importerar namnrymden för grundläggande systemklasser, inklusive Guid
using System.Collections.Generic;  // Importerar namnrymden för generiska kollektioner
using System.Linq;     // Importerar namnrymden för LINQ (Language-Integrated Query)
using System.Text;     // Importerar namnrymden för textbehandling
using System.Threading.Tasks;  // Importerar namnrymden för asynkrona uppgifter
using MediatR;         // Importerar namnrymden för MediatR för att stödja Mediator pattern

// Placerar koden i namnrymden "Application.Commands.Users.DeleteUser"
namespace Application.Commands.Users.DeleteUser
{
    // Skapar en klass "DeleteUserByIdCommand" som är en implementation av IRequest<T>
    // där T är typen av svar som förväntas från MediatR, i det här fallet en bool
    public class DeleteUserByIdCommand : IRequest<bool>
    {
        // Konstruktor för att skapa ett nytt DeleteUserByIdCommand-objekt med en användar-ID som parameter
        public DeleteUserByIdCommand(Guid deletedById)
        {
            // Tilldelar det inkommande användar-ID till den publika egenskapen DeletedById
            DeletedById = deletedById;
        }

        // Publik egenskap (property) för att få åtkomst till det inkommande användar-ID:t
        public Guid DeletedById { get; }
    }
}
