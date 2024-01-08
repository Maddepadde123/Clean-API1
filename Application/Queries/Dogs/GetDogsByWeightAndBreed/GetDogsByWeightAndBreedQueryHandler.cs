using Domain.Models;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.Dogs.GetDogsByWeightAndBreed
{
    public class GetDogsByWeightAndBreedQueryHandler : IRequestHandler<GetDogsByWeightAndBreedQuery, List<Dog>>
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ILogger<GetDogsByWeightAndBreedQueryHandler> _logger;

        public GetDogsByWeightAndBreedQueryHandler(IAnimalRepository animalRepository, ILogger<GetDogsByWeightAndBreedQueryHandler> logger)
        {
            _animalRepository = animalRepository;
            _logger = logger;
        }

        public async Task<List<Dog>> Handle(GetDogsByWeightAndBreedQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<Dog> dogsByWeightAndBreed = await _animalRepository.GetDogsByWeightAndBreedAsync(request.DogBreed, request.DogWeight);
                return dogsByWeightAndBreed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GetDogsByWeightAndBreedQueryHandler");
                throw;
            }
        }
    }
}
