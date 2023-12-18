using Domain.Models.AnimalUser;

namespace Domain.Models.User
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public ICollection<AnimalUserModel> AnimalUsers { get; set; } = new List<AnimalUserModel>();
    }
}
