// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using MediatR;  // Importerar namnrymden för MediatR för att stödja Mediator pattern

// Placerar koden i namnrymden "Application.Commands.Cats.DeleteDog"
namespace Application.Commands.Cats.DeleteDog
{
    // Skapar en klass "DeleteCatByIdCommand" som är en implementation av IRequest<T>
    // där T är typen av svar som förväntas från MediatR, i det här fallet en bool
    public class DeleteCatByIdCommand : IRequest<bool>
    {
        // Konstruktor för att skapa ett nytt DeleteCatByIdCommand-objekt med ett Cat ID som parameter
        public DeleteCatByIdCommand(Guid deletedCatId)
        {
            // Tilldelar det inkommande Cat ID till den publika egenskapen DeletedCatId
            DeletedCatId = deletedCatId;
        }

        // Publik egenskap (property) för att få åtkomst till det inkommande Cat ID
        public Guid DeletedCatId { get; }
    }
}
