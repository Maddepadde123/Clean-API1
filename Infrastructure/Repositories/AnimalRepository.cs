using Domain.Data;
using Domain.Models;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories.AnimalRepository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly AnimalDbContext _animalDbContext;
        private readonly ILogger<AnimalRepository> _logger;

        public AnimalRepository(AnimalDbContext animalDbContext, ILogger<AnimalRepository> logger)
        {
            _animalDbContext = animalDbContext;
            _logger = logger;
        }

        public async Task<List<Dog>> GetAllDogs()
        {
            try
            {
                List<Dog> allDogsFromDatabase = _animalDbContext.Dogs.ToList();
                return await Task.FromResult(allDogsFromDatabase);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting all dogs from the database");
                throw new Exception("An error occurred while getting all dogs from the databas", ex);
            }
        }

        public async Task<Dog> GetDogById(Guid dogId)
        {
            try
            {
                List<Dog> allDogsFromDatabase = _animalDbContext.Dogs.ToList();

                Dog wantedDog = allDogsFromDatabase.FirstOrDefault(dog => dog.Id == dogId)!;

                return await Task.FromResult(wantedDog);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting a dog by Id {dogId} from the database");
                throw new Exception($"An error occurred while getting a dog by Id {dogId} from the databas", ex);
            }
        }

        public async Task AddNewDog(Dog newDog)
        {
            try
            {
                _animalDbContext.Dogs.Add(newDog);
                await _animalDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while adding a new dog to the database");
                throw new Exception("An error occurred while adding a new dog to the database", ex);
            }
        }

        public async Task DeleteDogById(Guid dogId)
        {
            try
            {
                Dog dogToDelete = await _animalDbContext.Dogs.FindAsync(dogId);

                if (dogToDelete != null)
                {
                    _animalDbContext.Dogs.Remove(dogToDelete);
                    await _animalDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting a dog by Id {dogId} from the database");
                throw new Exception($"An error occurred while deleting a dog by Id {dogId} from the database", ex);
            }
        }

        public async Task UpdateDog(Dog updatedDog)
        {
            try
            {
                Dog existingDog = await _animalDbContext.Dogs.FindAsync(updatedDog.Id);

                if (existingDog != null)
                {
                    existingDog.Name = updatedDog.Name;
                    // Du kan också uppdatera andra egenskaper här efter behov

                    await _animalDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating a dog by Id {updatedDog.Id} in the database");
                throw new Exception($"An error occurred while updating a dog by Id {updatedDog.Id} in the database", ex);
            }
        }

        public async Task AddNewBird(Bird newBird)
        {
            try
            {
                _animalDbContext.Birds.Add(newBird);
                await _animalDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while adding a new bird to the database");
                throw new Exception("An error occurred while adding a new bird to the database", ex);
            }
        }

        public async Task DeleteBirdById(Guid birdId)
        {
            try
            {
                Bird birdToDelete = await _animalDbContext.Birds.FindAsync(birdId);

                if (birdToDelete != null)
                {
                    _animalDbContext.Birds.Remove(birdToDelete);
                    await _animalDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting a bird by Id {birdId} from the database");
                throw new Exception($"An error occurred while deleting a bird by Id {birdId} from the database", ex);
            }
        }

        public async Task UpdateBirdById(Bird updatedBird)
        {
            try
            {
                Bird existingBird = await _animalDbContext.Birds.FindAsync(updatedBird.Id);

                if (existingBird != null)
                {
                    existingBird.Name = updatedBird.Name;
                    // Uppdatera andra egenskaper efter behov

                    await _animalDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating a bird by Id {updatedBird.Id} in the database");
                throw new Exception($"An error occurred while updating a bird by Id {updatedBird.Id} in the database", ex);
            }
        }

        public async Task<List<Bird>> GetAllBirds()
        {
            try
            {
                List<Bird> allBirdsFromDatabase = _animalDbContext.Birds.ToList();
                return await Task.FromResult(allBirdsFromDatabase);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting all birds from the database");
                throw new Exception("An error occurred while getting all birds from the database", ex);
            }
        }

        public async Task<Bird> GetBirdById(Guid birdId)
        {
            try
            {
                Bird wantedBird = await _animalDbContext.Birds.FindAsync(birdId);

                if (wantedBird == null)
                {
                    _logger.LogError($"Bird with Id {birdId} not found in the database");
                    return null; // Eller hantera på något annat sätt, beroende på din logik
                }

                return wantedBird;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting a bird by Id {birdId} from the database");
                throw new Exception($"An error occurred while getting a bird by Id {birdId} from the database", ex);
            }
        }

        public async Task AddNewCat(Cat newCat)
        {
            try
            {
                _animalDbContext.Cats.Add(newCat);
                await _animalDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while adding a new cat to the database");
                throw new Exception("An error occurred while adding a new cat to the database", ex);
            }
        }

        public async Task DeleteCatById(Guid catId)
        {
            try
            {
                Cat catToDelete = await _animalDbContext.Cats.FindAsync(catId);

                if (catToDelete != null)
                {
                    _animalDbContext.Cats.Remove(catToDelete);
                    await _animalDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting a cat by Id {catId} from the database");
                throw new Exception($"An error occurred while deleting a cat by Id {catId} from the database", ex);
            }
        }

        public async Task UpdateCatById(Cat updatedCat)
        {
            try
            {
                Cat existingCat = await _animalDbContext.Cats.FindAsync(updatedCat.Id);

                if (existingCat != null)
                {
                    existingCat.Name = updatedCat.Name;
                    // Uppdatera andra egenskaper efter behov

                    await _animalDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating a cat by Id {updatedCat.Id} in the database");
                throw new Exception($"An error occurred while updating a cat by Id {updatedCat.Id} in the database", ex);
            }
        }

        public async Task<List<Cat>> GetAllCats()
        {
            try
            {
                List<Cat> allCatsFromDatabase = _animalDbContext.Cats.ToList();
                return await Task.FromResult(allCatsFromDatabase);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting all cats from the database");
                throw new Exception("An error occurred while getting all cats from the database", ex);
            }
        }

        public async Task<Cat> GetCatById(Guid catId)
        {
            try
            {
                Cat wantedCat = await _animalDbContext.Cats.FindAsync(catId);

                if (wantedCat == null)
                {
                    _logger.LogError($"Cat with Id {catId} not found in the database");
                    return null; // eller hantera på något annat sätt, beroende på din logik
                }

                return wantedCat;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting a cat by Id {catId} from the database");
                throw new Exception($"An error occurred while getting a cat by Id {catId} from the database", ex);
            }
        }

    }
}
