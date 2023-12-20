using Domain.Models;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Birds
{
    public class AddBirdCommandHandler : IRequestHandler<AddBirdCommand, Bird>
    {
        private readonly IAnimalRepository _animalRepository;

        public AddBirdCommandHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Bird> Handle(AddBirdCommand request, CancellationToken cancellationToken)
        {
            Bird birdToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewBird.Name,
                CanFly = request.NewBird.CanFly,
                Color = request.NewBird.Color,
            };

            await _animalRepository.AddNewBird(birdToCreate);

            return birdToCreate;
        }
    }
}
