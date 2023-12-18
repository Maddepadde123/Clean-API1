using Domain.Models.AnimalUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
