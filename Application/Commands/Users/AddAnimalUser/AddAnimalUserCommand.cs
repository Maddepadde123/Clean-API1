using Application.Dtos;
using Domain.Models.AnimalUser;
using MediatR;

namespace Application.Commands.Users.AddAnimalUser
{
    public class AddAnimalUserCommand : IRequest<AnimalUserModel>
    {
        public AddAnimalUserCommand(AnimalUserDto animalUserDto)
        {
            UserId = animalUserDto.UserId;
            AnimalId = animalUserDto.AnimalId;
        }
        public Guid AnimalId { get; set; }
        public Guid UserId { get; set; }
    }
}
