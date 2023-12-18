using Application.Queries.Users.GetById;
using Domain.Models.User;
using Infrastructure.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

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
