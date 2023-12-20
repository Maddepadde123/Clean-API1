using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Dogs.DeleteDog
{
    public class DeleteDogByIdCommandHandler : IRequestHandler<DeleteDogByIdCommand, bool>
    {
        private readonly IAnimalRepository _animalRepository;

        public DeleteDogByIdCommandHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<bool> Handle(DeleteDogByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _animalRepository.DeleteDogById(request.DeletedDogId);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
