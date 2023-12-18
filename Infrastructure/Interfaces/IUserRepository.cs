// IUserRepository.cs
using Domain.Models.User;

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
