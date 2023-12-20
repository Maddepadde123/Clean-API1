using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Birds.DeleteDog
{
    public class DeleteBirdByIdCommandHandler : IRequestHandler<DeleteBirdByIdCommand, bool>
    {
        private readonly IAnimalRepository _animalRepository;

        public DeleteBirdByIdCommandHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<bool> Handle(DeleteBirdByIdCommand request, CancellationToken cancellationToken)
        {
            await _animalRepository.DeleteBirdById(request.DeletedBirdId);

            return true;
        }
    }
}
