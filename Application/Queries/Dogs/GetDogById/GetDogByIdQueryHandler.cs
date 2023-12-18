using Domain.Models;
using Infrastructure.Interfaces;  // Lägg till referensen till ditt Interface
using MediatR;

namespace Application.Queries.Dogs.GetById
{
    public class GetDogByIdQueryHandler : IRequestHandler<GetDogByIdQuery, Dog>
    {
        private readonly IAnimalRepository _animalRepository;

        public GetDogByIdQueryHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Dog> Handle(GetDogByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Använd AnimalRepository för att hämta hunden från databasen
                Dog wantedDog = await _animalRepository.GetDogById(request.Id);
                return wantedDog;
            }
            catch (Exception ex)
            {
                // Logga och hantera fel här
                throw;
            }
        }
    }
}

