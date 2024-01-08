using Application.Queries.Cats.GetCatsByWeightAndBreed;
using Domain.Models;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Animals.Queries.Cats.GetByWeightBreed
{
    public class GetCatsByWeightAndBreedQueryHandler : IRequestHandler<GetCatsByWeightAndBreedQuery, List<Cat>>
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly ILogger<GetCatsByWeightAndBreedQueryHandler> _logger;

        public GetCatsByWeightAndBreedQueryHandler(IAnimalRepository animalRepository, ILogger<GetCatsByWeightAndBreedQueryHandler> logger)
        {
            _animalRepository = animalRepository;
            _logger = logger;
        }

        public async Task<List<Cat>> Handle(GetCatsByWeightAndBreedQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<Cat> catsByWeightBreed = await _animalRepository.GetCatsByWeightAndBreedAsync(request.CatBreed, request.CatWeight);
                return catsByWeightBreed;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GetCatsByWeightAndBreedQueryHandler");
                throw;
            }
        }
    }
}
