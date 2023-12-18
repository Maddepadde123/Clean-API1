using Domain.Models;
using Infrastructure.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Birds.GetById
{
    public class GetBirdByIdQueryHandler : IRequestHandler<GetBirdByIdQuery, Bird>
    {
        private readonly IAnimalRepository _animalRepository;

        public GetBirdByIdQueryHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Bird> Handle(GetBirdByIdQuery request, CancellationToken cancellationToken)
        {
            // Anropa din repository för att hämta fågeln med det angivna ID
            Bird wantedBird = await _animalRepository.GetBirdById(request.Id);

            return wantedBird;
        }
    }
}
