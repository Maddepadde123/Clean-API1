using Domain.Models.User;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Users.DeleteUser
{
    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserByIdCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                UserModel userToDelete = await _userRepository.GetUserById(request.DeletedById);

                if (userToDelete != null)
                {
                    await _userRepository.DeleteUserById(request.DeletedById);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
