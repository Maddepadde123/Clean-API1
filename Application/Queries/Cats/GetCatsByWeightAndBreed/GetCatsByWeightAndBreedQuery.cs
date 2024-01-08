// GetCatsByWeightAndBreedQuery.cs
using Domain.Models;
using MediatR;

namespace Application.Queries.Cats.GetCatsByWeightAndBreed
{
    public class GetCatsByWeightAndBreedQuery : IRequest<List<Cat>>
    {
        public string? CatBreed { get; set; }
        public int? CatWeight { get; set; }
    }
}
