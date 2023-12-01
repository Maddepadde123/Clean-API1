using MediatR;

namespace Application.Commands.Birds.DeleteDog
{
    public class DeleteBirdByIdCommand : IRequest<bool>
    {
        public DeleteBirdByIdCommand(Guid deletedBirdId)
        {
            DeletedBirdId = deletedBirdId;
        }

        public Guid DeletedBirdId { get; }
    }
}