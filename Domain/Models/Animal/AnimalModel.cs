using Domain.Models.AnimalUser;

namespace Domain.Models.Animal
{
    public class AnimalModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<AnimalUserModel> AnimalUsers { get; set; } = new List<AnimalUserModel>();
    }
}
