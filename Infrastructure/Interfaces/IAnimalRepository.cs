using Domain.Models;
namespace Infrastructure.Interfaces
{
    public interface IAnimalRepository
    {
        Task AddNewBird(Bird birdToCreate);
        Task AddNewCat(Cat catToCreate);
        Task AddNewDog(Dog dogToCreate);
        Task DeleteBirdById(Guid deletedBirdId);
        Task DeleteCatById(Guid deletedCatId);
        Task DeleteDogById(Guid deletedDogId);
        Task<List<Bird>> GetAllBirds();
        Task<List<Cat>> GetAllCats();
        Task<List<Dog>> GetAllDogs();
        Task<Bird> GetBirdById(Guid id);
        Task<List<Bird>> GetBirdsByColorAsync(string color);
        Task<Cat> GetCatById(Guid id);
        Task<List<Cat>> GetCatsByWeightAndBreedAsync(string? breed, int? weight);
        Task<Dog> GetDogById(Guid dogId);
        Task<List<Dog>> GetDogsByWeightAndBreedAsync(string dogBreed, int? dogWeight);
        Task UpdateBirdById(Bird birdToUpdate);
        Task UpdateCatById(Cat existingCat);
        Task UpdateDog(Dog updatedDog);
    }
}

