// Använder nödvändig namnrymd för MediatR för att stödja Mediator pattern
using MediatR;

// Placerar koden i namnrymden "Application.Commands.Dogs.DeleteDog"
namespace Application.Commands.Dogs.DeleteDog
{
    // Skapar en klass "DeleteDogByIdCommand" som är en implementation av IRequest<T>
    // där T är typen av svar som förväntas från MediatR, i det här fallet en bool
    public class DeleteDogByIdCommand : IRequest<bool>
    {
        // Konstruktor för att skapa ett nytt DeleteDogByIdCommand-objekt med ett Dog ID som parameter
        public DeleteDogByIdCommand(Guid deletedDogId)
        {
            // Tilldelar det inkommande Dog ID till den publika egenskapen DeletedDogId
            DeletedDogId = deletedDogId;
        }

        // Publik egenskap (property) för att få åtkomst till det inkommande Dog ID
        public Guid DeletedDogId { get; }
    }
}
