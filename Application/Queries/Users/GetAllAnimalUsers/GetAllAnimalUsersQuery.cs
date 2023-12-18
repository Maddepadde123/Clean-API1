using Domain.Models.AnimalUser;
using MediatR;

namespace Application.Queries.Users.GetAllAnimalUsers
{
    public class GetAllAnimalUsersQuery : IRequest<List<AnimalUserModel>>
    {
    }
}
