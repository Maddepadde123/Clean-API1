// UserRepository.cs
using Domain.Data;
using Domain.Models.User;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AnimalDbContext _animalDbContext;

        public UserRepository(AnimalDbContext animalDbContext)
        {
            _animalDbContext = animalDbContext;
        }

        public async Task<UserModel> GetUserById(Guid userId)
        {
            return await _animalDbContext.UserModel.FindAsync(userId);
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _animalDbContext.UserModel.ToListAsync();
        }

        public async Task RegisterUser(UserModel newUser)
        {
            await _animalDbContext.UserModel.AddAsync(newUser);
            await _animalDbContext.SaveChangesAsync();
        }

        public async Task DeleteUserById(Guid userId)
        {
            var userToDelete = await _animalDbContext.UserModel.FindAsync(userId);

            if (userToDelete != null)
            {
                _animalDbContext.UserModel.Remove(userToDelete);
                await _animalDbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateUserById(UserModel updatedUser)
        {
            _animalDbContext.UserModel.Update(updatedUser);
            await _animalDbContext.SaveChangesAsync();
        }

        public UserModel GetUserByUsernameAndPassword(string userName, string password)
        {
            return _animalDbContext.UserModel.FirstOrDefault(u => u.UserName == userName && u.UserPassword == password);
        }
    }
}
