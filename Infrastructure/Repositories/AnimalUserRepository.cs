using Domain.Data;
using Domain.Models.AnimalUser;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.AnimalUserRepository
{
    public class AnimalUserRepository : IAnimalUserRepository
    {
        private readonly AnimalDbContext _animalDbContext;
        private readonly ILogger<AnimalUserRepository> _logger;

        public AnimalUserRepository(AnimalDbContext animalDbContext, ILogger<AnimalUserRepository> logger)
        {
            _animalDbContext = animalDbContext;
            _logger = logger;
        }

        public async Task<AnimalUserModel> GetAnimalUserById(Guid userId, Guid animalId)
        {
            try
            {
                AnimalUserModel animalUser = await _animalDbContext.AnimalUserModels
                    .Include(au => au.Animal)
                    .Include(au => au.User)
                    .FirstOrDefaultAsync(au => au.UserId == userId && au.AnimalId == animalId);

                return animalUser;
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while getting AnimalUser by UserId {userId} and AnimalId {animalId} from the database");
                throw new Exception($"An error occurred while getting AnimalUser by UserId {userId} and AnimalId {animalId} from the database", ex);
            }
        }

        public async Task<List<AnimalUserModel>> GetAllAnimalUsers()
        {
            try
            {
                var allAnimalUsers = await _animalDbContext.AnimalUserModels.Select(au => new AnimalUserModel
                {
                    AnimalId = au.AnimalId,
                    UserId = au.UserId

                }).ToListAsync();



                return allAnimalUsers;
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting all AnimalUsers from the database");
                throw new Exception("An error occurred while getting all AnimalUsers from the database", ex);
            }
        }



        public async Task DeleteAnimalUserById(Guid userId, Guid animalId)
        {
            try
            {
                AnimalUserModel animalUserToDelete = await _animalDbContext.AnimalUserModels
                    .FirstOrDefaultAsync(au => au.UserId == userId && au.AnimalId == animalId);

                if (animalUserToDelete != null)
                {
                    _animalDbContext.AnimalUserModels.Remove(animalUserToDelete);
                    await _animalDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting AnimalUser by UserId {userId} and AnimalId {animalId} from the database");
                throw new Exception($"An error occurred while deleting AnimalUser by UserId {userId} and AnimalId {animalId} from the database", ex);
            }
        }

        public async Task UpdateAnimalUser(AnimalUserModel updatedAnimalUser)
        {
            try
            {
                AnimalUserModel existingAnimalUser = await _animalDbContext.AnimalUserModels
                    .FirstOrDefaultAsync(au => au.UserId == updatedAnimalUser.UserId && au.AnimalId == updatedAnimalUser.AnimalId);

                if (existingAnimalUser != null)
                {
                    await _animalDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating AnimalUser by UserId {updatedAnimalUser.UserId} and AnimalId {updatedAnimalUser.AnimalId} in the database");
                throw new Exception($"An error occurred while updating AnimalUser by UserId {updatedAnimalUser.UserId} and AnimalId {updatedAnimalUser.AnimalId} in the database", ex);
            }
        }

      

       public async Task<AnimalUserModel> AddAnimalUserAsync(AnimalUserModel newAnimalUser)
        {

            try
            {
                _animalDbContext.AnimalUserModels.Add(newAnimalUser);

                await _animalDbContext.SaveChangesAsync();
                return newAnimalUser;
                
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while adding a new AnimalUser to the database");
                throw new Exception("An error occurred while adding a new AnimalUser to the database", ex);
            }
            

        }
    }
}
