using Domain.Models.AnimalUser;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Users.DeleteAnimalUser
{
    public class DeleteAnimalUserByIdCommandHandler : IRequestHandler<DeleteAnimalUserByIdCommand, bool>
    {
        private readonly IAnimalUserRepository _animalUserRepository;

        public DeleteAnimalUserByIdCommandHandler(IAnimalUserRepository animalUserRepository)
        {
            _animalUserRepository = animalUserRepository;
        }

        public async Task<bool> Handle(DeleteAnimalUserByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                AnimalUserModel animalUserToDelete = await _animalUserRepository.GetAnimalUserById(request.UserId, request.AnimalId);

                if (animalUserToDelete != null)
                {
                    await _animalUserRepository.DeleteAnimalUserById(request.UserId, request.AnimalId);
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
