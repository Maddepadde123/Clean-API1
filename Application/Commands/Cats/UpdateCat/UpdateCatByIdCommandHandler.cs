using Domain.Models;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Commands.Cats.UpdateCat
{
    public class UpdateCatByIdCommandHandler : IRequestHandler<UpdateCatByIdCommand, Cat>
    {
        private readonly IAnimalRepository _animalRepository;

        public UpdateCatByIdCommandHandler(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public async Task<Cat> Handle(UpdateCatByIdCommand request, CancellationToken cancellationToken)
        {
            Cat existingCat = await _animalRepository.GetCatById(request.Id);

            if (existingCat == null)
            {
                return null;
            }

            existingCat.Name = request.UpdatedCat.Name;
            existingCat.LikesToPlay = request.UpdatedCat.LikesToPlay;
            existingCat.CatBreed = request.UpdatedCat.CatBreed;
            existingCat.CatWeight = request.UpdatedCat.CatWeight;

            await _animalRepository.UpdateCatById(existingCat);

            return existingCat;
        }
    }
}
