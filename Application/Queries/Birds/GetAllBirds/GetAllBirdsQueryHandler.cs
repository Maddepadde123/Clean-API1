using Application.Queries.Birds.GetAll;
using Domain.Models;
using Infrastructure.Interfaces;
using Infrastructure.Repositories.AnimalRepository; // Lägg till rätt namespace för ditt repository
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Birds
{
    public class GetAllBirdsQueryHandler : IRequestHandler<GetAllBirdsQuery, List<Bird>>
    {
        private readonly IAnimalRepository _animalRepository;

        public GetAllBirdsQueryHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<List<Bird>> Handle(GetAllBirdsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<Bird> allBirdsFromDatabase = await _animalRepository.GetAllBirds();
                return allBirdsFromDatabase;
            }
            catch (Exception ex)
            {
                // Hantera fel här om det behövs
                // Logga ex.Message eller använd annan felhantering
                throw;
            }
        }
    }
}
