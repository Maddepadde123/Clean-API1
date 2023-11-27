using Application.Dtos;
using MediatR;

namespace Application.Commands.Cats.DeleteDog
{
    public class DeleteCatByIdCommand : IRequest<bool>
    {
        public DeleteCatByIdCommand(CatDto deletedCat, Guid deletedCatId)
        {
            DeletedCat = deletedCat;
            DeletedCatId = deletedCatId;
        }
        public CatDto DeletedCat { get; }
        public Guid DeletedCatId { get; }
    }
}