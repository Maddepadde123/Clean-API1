using Domain.Models.AnimalUser;

namespace Infrastructure.Interfaces
{
    public interface IAnimalUserRepository
    {
        Task<AnimalUserModel> AddAnimalUserAsync(AnimalUserModel newAnimalUser);
        Task DeleteAnimalUserById(Guid userId, Guid animalId);
        Task<List<AnimalUserModel>> GetAllAnimalUsers();
        Task<AnimalUserModel> GetAnimalUserById(Guid userId, Guid animalId);
        Task UpdateAnimalUser(AnimalUserModel existingAnimalUser);
    }
}
