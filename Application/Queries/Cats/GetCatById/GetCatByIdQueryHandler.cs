using Application.Queries.Cats.GetById;
using Domain.Models;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Queries.Cats
{
    public class GetCatByIdQueryHandler : IRequestHandler<GetCatByIdQuery, Cat>
    {
        private readonly IAnimalRepository _animalRepository;

        public GetCatByIdQueryHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Cat> Handle(GetCatByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Cat cat = await _animalRepository.GetCatById(request.Id);
                return cat;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while processing GetCatByIdQuery: {ex.Message}", ex);
            }
        }

    }
}
