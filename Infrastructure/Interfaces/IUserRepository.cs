// IUserRepository.cs
using Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserById(Guid userId);
        Task<List<UserModel>> GetAllUsers();
        Task RegisterUser(UserModel newUser);
        Task DeleteUserById(Guid userId);
        Task UpdateUserById(UserModel updatedUser);
        UserModel GetUserByUsernameAndPassword(string userName, string password);
        
    }
}
