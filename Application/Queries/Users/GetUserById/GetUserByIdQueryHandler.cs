using Domain.Models.User;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Queries.Users.GetById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserModel>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Use UserRepository to fetch the user from the database
                UserModel wantedUser = await _userRepository.GetUserById(request.Id);
                return wantedUser;
            }
            catch (Exception ex)
            {
                // Log and handle errors here
                throw;
            }
        }
    }
}
