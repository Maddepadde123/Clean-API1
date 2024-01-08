using Domain.Models;
using MediatR;

namespace Application.Queries.Birds.GetBirdByColor
{
    public class GetBirdByColorQuery : IRequest<List<Bird>>
    {
        public string Color { get; set; }
    }
}