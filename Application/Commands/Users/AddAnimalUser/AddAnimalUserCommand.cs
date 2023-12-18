using Application.Dtos;
using Domain.Models.AnimalUser;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
