using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Domain.Models;
using MediatR;
using Application.Queries.Cats.GetAll;

namespace Application.Queries.Cats
{
    public class GetAllCatsQueryHandler : IRequestHandler<GetAllCatsQuery, List<Cat>>
    {
        private readonly IAnimalRepository _animalRepository;

        public GetAllCatsQueryHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<List<Cat>> Handle(GetAllCatsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Använd GetAllCats-metoden från din AnimalRepository för att hämta alla katter från databasen
                List<Cat> allCatsFromDatabase = await _animalRepository.GetAllCats();
                return allCatsFromDatabase;
            }
            catch (Exception ex)
            {
                // Hantera eventuella fel här, logga eller kasta exception
                // Du kan också använda en egen Exception-klass om du har en definierad sådan
                throw new Exception("An error occurred while getting all cats from the database", ex);
            }
        }
    }
}
