using Domain.Models.User;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Users.GetAll
{
    public class GetAllUsersQuery : IRequest<List<UserModel>>
    {
    }
}
