using Domain.Models.AnimalUser;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Users.AddAnimalUser
{
    public class AddAnimalUserCommandHandler : IRequestHandler<AddAnimalUserCommand, AnimalUserModel>
    {
        private readonly IAnimalUserRepository _userRepository;
        public AddAnimalUserCommandHandler(IAnimalUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AnimalUserModel> Handle(AddAnimalUserCommand request, CancellationToken cancellationToken)
        {
            var animalUserModel = new AnimalUserModel
            {
                UserId = request.UserId,
                AnimalId = request.AnimalId,
            };
            return await _userRepository.AddAnimalUserAsync(animalUserModel);
        }
    }
}
