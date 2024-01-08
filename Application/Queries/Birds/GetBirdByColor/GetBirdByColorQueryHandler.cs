using Application.Queries.Birds.GetBirdByColor;
using Domain.Models;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetBirdByColorQueryHandler : IRequestHandler<GetBirdByColorQuery, List<Bird>>
{
    private readonly IAnimalRepository _animalRepository;
    private readonly ILogger<GetBirdByColorQueryHandler> _logger;

    public GetBirdByColorQueryHandler(IAnimalRepository animalRepository, ILogger<GetBirdByColorQueryHandler> logger)
    {
        _animalRepository = animalRepository;
        _logger = logger;
    }

    public async Task<List<Bird>> Handle(GetBirdByColorQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var birds = await _animalRepository.GetBirdsByColorAsync(request.Color);
            var sortedBirds = birds.OrderByDescending(bird => bird.Name).ToList();
            return sortedBirds;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in GetBirdByColor: {ex.Message}");
            throw;
        }
    }
}
