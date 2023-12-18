using Domain.Models.AnimalUser;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Queries.Users.GetAnimalUserById
{
    public class GetAnimalUserByIdQueryHandler : IRequestHandler<GetAnimalUserByIdQuery, AnimalUserModel>
    {
        private readonly IAnimalUserRepository _animalUserRepository;

        public GetAnimalUserByIdQueryHandler(IAnimalUserRepository animalUserRepository)
        {
            _animalUserRepository = animalUserRepository;
        }

        public async Task<AnimalUserModel> Handle(GetAnimalUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Använd AnimalUserRepository för att hämta den specifika djur-användaren från databasen
                AnimalUserModel wantedAnimalUser = await _animalUserRepository.GetAnimalUserById(request.UserId, request.AnimalId);
                return wantedAnimalUser;
            }
            catch (Exception ex)
            {
                // Logga och hantera fel här
                throw;
            }
        }
    }
}
