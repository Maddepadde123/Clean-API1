﻿using Application.Queries.Cats.GetById;
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
                // Använd GetCatById-metoden från din AnimalRepository för att hämta katten från databasen
                Cat cat = await _animalRepository.GetCatById(request.Id);
                return cat;
            }
            catch (Exception ex)
            {
                // Hantera eventuella fel här, logga eller kasta exception
                // Du kan också använda en egen Exception-klass om du har en definierad sådan
                throw new Exception($"An error occurred while processing GetCatByIdQuery: {ex.Message}", ex);
            }
        }

    }
}
