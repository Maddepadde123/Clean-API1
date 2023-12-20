using Domain.Models;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Birds.UpdateBird
{
    public class UpdateBirdByIdCommandHandler : IRequestHandler<UpdateBirdByIdCommand, Bird>
    {
        private readonly IAnimalRepository _animalRepository;

        public UpdateBirdByIdCommandHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository ?? throw new ArgumentNullException(nameof(animalRepository));
        }

        public async Task<Bird> Handle(UpdateBirdByIdCommand request, CancellationToken cancellationToken)
        {
            Bird birdToUpdate = await _animalRepository.GetBirdById(request.Id);

            if (birdToUpdate != null)
            {
                birdToUpdate.Name = request.UpdatedBird.Name;
                birdToUpdate.CanFly = request.UpdatedBird.CanFly;
                birdToUpdate.Color = request.UpdatedBird.Color;

                await _animalRepository.UpdateBirdById(birdToUpdate);

                return birdToUpdate;
            }

            return null;
        }
    }
}
