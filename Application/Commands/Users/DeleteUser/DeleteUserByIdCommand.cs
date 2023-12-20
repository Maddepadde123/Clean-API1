using MediatR;

namespace Application.Commands.Users.DeleteUser
{
    public class DeleteUserByIdCommand : IRequest<bool>
    {
        public DeleteUserByIdCommand(Guid deletedById)
        {
            DeletedById = deletedById;
        }

        public Guid DeletedById { get; }
    }
}
