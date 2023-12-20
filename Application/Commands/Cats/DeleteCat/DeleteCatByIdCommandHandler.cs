using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Cats.DeleteDog
{
    public class DeleteCatByIdCommandHandler : IRequestHandler<DeleteCatByIdCommand, bool>
    {
        private readonly IAnimalRepository _animalRepository;

        public DeleteCatByIdCommandHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<bool> Handle(DeleteCatByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _animalRepository.DeleteCatById(request.DeletedCatId);
                return true;
            }
            catch (Exception ex)
            {
                // Log or handle errors if they occur and return false
                // This prevents errors in the deletion process from crashing the application
                // You can customize this part based on your logging and error-handling needs
                return false;
            }
        }
    }
}
