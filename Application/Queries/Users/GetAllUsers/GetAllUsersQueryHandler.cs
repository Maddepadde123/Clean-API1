using Application.Queries.Users.GetAll;
using Domain.Models.User;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Queries.Users
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserModel>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Use UserRepository to fetch all users from the database
                List<UserModel> allUsersFromDatabase = await _userRepository.GetAllUsers();
                return allUsersFromDatabase;
            }
            catch (Exception ex)
            {
                // Log and handle errors here
                throw;
            }
        }
    }
}
