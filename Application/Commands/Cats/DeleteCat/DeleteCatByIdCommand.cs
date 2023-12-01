using MediatR;

namespace Application.Commands.Cats.DeleteDog
{
    public class DeleteCatByIdCommand : IRequest<bool>
    {
        public DeleteCatByIdCommand(Guid deletedCatId)
        {
            DeletedCatId = deletedCatId;
        }
        public Guid DeletedCatId { get; }
    }
}