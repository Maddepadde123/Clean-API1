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
                // Hämtar den befintliga hunden från databasen baserat på det angivna ID:t
                Dog existingDog = await _animalRepository.GetDogById(request.Id);

                // Kontrollerar om hunden finns i databasen
                if (existingDog == null)
                {
                    // Hunden med det angivna ID:t finns inte i databasen
                    // Du kan hantera detta på lämpligt sätt, t.ex. kasta ett undantag eller returnera null
                    return null;
                }

                // Uppdaterar hundens egenskaper med informationen från den uppdaterade hunden
                existingDog.Name = request.UpdatedDog.Name;
                existingDog.DogBreed = request.UpdatedDog.DogBreed;
                existingDog.DogWeight = request.UpdatedDog.DogWeight;

                // Uppdaterar hunden i databasen
                await _animalRepository.UpdateDog(existingDog);

                // Returnerar den uppdaterade hunden
                return existingDog;
            }
            catch (Exception ex)
            {
                // Loggar och kastar vidare eventuella fel som uppstår under uppdateringsprocessen
                throw;
            }
        }
    }
}
