using MediatR;

namespace Application.Commands.Users.DeleteAnimalUser
{
    public class DeleteAnimalUserByIdCommand : IRequest<bool>
    {
        public DeleteAnimalUserByIdCommand(Guid userId, Guid animalId)
        {
            UserId = userId;
            AnimalId = animalId;
        }

        public Guid UserId { get; }
        public Guid AnimalId { get; }
    }
}
