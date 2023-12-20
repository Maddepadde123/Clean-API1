using Domain.Models;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Dogs
{
    public class AddDogCommandHandler : IRequestHandler<AddDogCommand, Dog>
    {
        private readonly IAnimalRepository _animalRepository;

        public AddDogCommandHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Dog> Handle(AddDogCommand request, CancellationToken cancellationToken)
        {
            Dog dogToCreate = new()
            {
                Id = Guid.NewGuid(),
                Name = request.NewDog.Name,
                DogBreed = request.NewDog.DogBreed,
                DogWeight = request.NewDog.DogWeight,
            };

            await _animalRepository.AddNewDog(dogToCreate);

            return dogToCreate;
        }
    }
}
