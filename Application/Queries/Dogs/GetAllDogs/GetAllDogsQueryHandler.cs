
using Application.Queries.Dogs.GetAll;
using Domain.Models;
using Infrastructure.Interfaces;  // Lägg till referensen till ditt Interface
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.Dogs
{
    public class GetAllDogsQueryHandler : IRequestHandler<GetAllDogsQuery, List<Dog>>
    {
        private readonly IAnimalRepository _animalRepository;

        public GetAllDogsQueryHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<List<Dog>> Handle(GetAllDogsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Använd AnimalRepository för att hämta alla hundar från databasen
                List<Dog> allDogsFromDatabase = await _animalRepository.GetAllDogs();
                return allDogsFromDatabase;
            }
            catch (Exception ex)
            {
                // Logga och hantera fel här
                throw new Exception("An error occurred while getting all cats from the database", ex);
            }
        }
    }
}

