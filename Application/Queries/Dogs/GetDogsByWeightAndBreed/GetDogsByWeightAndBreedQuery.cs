using Domain.Models;
using MediatR;

namespace Application.Queries.Dogs.GetDogsByWeightAndBreed
{
    public class GetDogsByWeightAndBreedQuery : IRequest<List<Dog>>
    {
        public string? DogBreed { get; set; }
        public int? DogWeight { get; set; }
    }
}
