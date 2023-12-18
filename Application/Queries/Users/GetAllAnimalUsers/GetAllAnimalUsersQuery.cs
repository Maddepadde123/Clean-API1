using Domain.Models.AnimalUser;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Users.GetAllAnimalUsers
{
    public class GetAllAnimalUsersQuery : IRequest<List<AnimalUserModel>>
    {
    }
}
