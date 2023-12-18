using Domain.Models.AnimalUser;
using MediatR;

namespace Application.Queries.Users.GetAnimalUserById
{
    public class GetAnimalUserByIdQuery : IRequest<AnimalUserModel>
    {
        public GetAnimalUserByIdQuery(Guid userId, Guid animalId)
        {
            UserId = userId;
            AnimalId = animalId;
        }

        public Guid UserId { get; }
        public Guid AnimalId { get; }
    }
}
