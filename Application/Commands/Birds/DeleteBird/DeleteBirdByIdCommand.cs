// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using MediatR;  // Importerar namnrymden för MediatR för att stödja Mediator pattern

// Placerar koden i namnrymden "Application.Commands.Birds.DeleteDog"
namespace Application.Commands.Birds.DeleteDog
{
    // Skapar en klass "DeleteBirdByIdCommand" som är en implementation av IRequest<T>
    // där T är typen av svar som förväntas från MediatR, i det här fallet en bool (true/false)
    public class DeleteBirdByIdCommand : IRequest<bool>
    {
        // Konstruktor för att skapa ett nytt DeleteBirdByIdCommand-objekt med ett Bird ID som parameter
        public DeleteBirdByIdCommand(Guid deletedBirdId)
        {
            // Tilldelar det inkommande Bird ID till den publika egenskapen DeletedBirdId
            DeletedBirdId = deletedBirdId;
        }

        // Publik egenskap (property) för att få åtkomst till det inkommande Bird ID
        public Guid DeletedBirdId { get; }
    }
}
