using Domain.Models.Animal;

namespace Domain.Models
{
    public class Cat : AnimalModel
    {
        public bool LikesToPlay { get; set; }
        public string CatBreed { get; set; }
        public int CatWeight { get; set; }
    }
}