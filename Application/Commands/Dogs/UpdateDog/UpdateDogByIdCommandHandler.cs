using Domain.Models;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Dogs.UpdateDog
{
    public class UpdateDogByIdCommandHandler : IRequestHandler<UpdateDogByIdCommand, Dog>
    {
        private readonly IAnimalRepository _animalRepository;

        public UpdateDogByIdCommandHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Dog> Handle(UpdateDogByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                Dog existingDog = await _animalRepository.GetDogById(request.Id);

                if (existingDog == null)
                {
                    return null;
                }

                existingDog.Name = request.UpdatedDog.Name;
                existingDog.DogBreed = request.UpdatedDog.DogBreed;
                existingDog.DogWeight = request.UpdatedDog.DogWeight;

                await _animalRepository.UpdateDog(existingDog);

                return existingDog;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
