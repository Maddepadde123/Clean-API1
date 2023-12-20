using Domain.Models;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Cats
{
    public class AddCatCommandHandler : IRequestHandler<AddCatCommand, Cat>
    {
        private readonly IAnimalRepository _animalRepository;

        public AddCatCommandHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Cat> Handle(AddCatCommand request, CancellationToken cancellationToken)
        {
            Cat catToCreate = new Cat
            {
                Id = Guid.NewGuid(),
                Name = request.NewCat.Name,
                LikesToPlay = request.NewCat.LikesToPlay,
                CatBreed = request.NewCat.CatBreed,
                CatWeight = request.NewCat.CatWeight,
            };

            await _animalRepository.AddNewCat(catToCreate);

            return catToCreate;
        }
    }
}
