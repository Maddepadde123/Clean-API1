using Application.Queries.Users.GetAllAnimalUsers;
using Domain.Models.AnimalUser;
using Infrastructure.Interfaces;
using MediatR;

namespace Application.Queries.Users
{
    public class GetAllAnimalUsersQueryHandler : IRequestHandler<GetAllAnimalUsersQuery, List<AnimalUserModel>>
    {
        private readonly IAnimalUserRepository _animalUserRepository;

        public GetAllAnimalUsersQueryHandler(IAnimalUserRepository animalUserRepository)
        {
            _animalUserRepository = animalUserRepository;
        }

        public async Task<List<AnimalUserModel>> Handle(GetAllAnimalUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Använd AnimalUserRepository för att hämta alla djur-användare från databasen
                List<AnimalUserModel> allAnimalUsersFromDatabase = await _animalUserRepository.GetAllAnimalUsers();
                return allAnimalUsersFromDatabase;
            }
            catch (Exception ex)
            {
                // Logga och hantera fel här
                throw;
            }
        }
    }
}
