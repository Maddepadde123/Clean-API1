using MediatR;

namespace Application.Queries.Users
{
    public class UserLoginQuery : IRequest<string>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }
}
