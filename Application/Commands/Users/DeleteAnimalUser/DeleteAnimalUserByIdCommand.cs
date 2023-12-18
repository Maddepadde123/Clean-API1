// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using MediatR;           // Importerar namnrymden för MediatR för att stödja Mediator pattern

// Placerar koden i namnrymden "Application.Commands.Users.DeleteAnimalUser"
namespace Application.Commands.Users.DeleteAnimalUser
{
    // Skapar en klass "DeleteAnimalUserByIdCommand" som är en implementation av IRequest<T>
    // där T är typen av svar som förväntas från MediatR, i det här fallet en bool
    public class DeleteAnimalUserByIdCommand : IRequest<bool>
    {
        // Konstruktor för att skapa ett nytt DeleteAnimalUserByIdCommand-objekt med ett användar-ID och djur-ID som parametrar
        public DeleteAnimalUserByIdCommand(Guid userId, Guid animalId)
        {
            // Tilldelar de inkommande användar-ID och djur-ID till de publika egenskaperna UserId och AnimalId
            UserId = userId;
            AnimalId = animalId;
        }

        // Publik egenskap (property) för att få åtkomst till det inkommande användar-ID:t
        public Guid UserId { get; }

        // Publik egenskap (property) för att få åtkomst till det inkommande djur-ID:t
        public Guid AnimalId { get; }
    }
}
