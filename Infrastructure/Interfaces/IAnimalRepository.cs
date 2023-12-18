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
        Task<Cat> GetCatById(Guid id);
        Task<Dog> GetDogById(Guid dogId);
        Task UpdateBirdById(Bird birdToUpdate);
        Task UpdateCatById(Cat existingCat);
        Task UpdateDog(Dog updatedDog);
    }
}

