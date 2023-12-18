using Application.Dtos;
using Domain.Models.AnimalUser;
using MediatR;
using System;

namespace Application.Commands.Users.UpdateAnimalUser
{
    public class UpdateAnimalUserByIdCommand : IRequest<AnimalUserModel>
    {
        public UpdateAnimalUserByIdCommand(AnimalUserDto updatedAnimalUser, Guid userId, Guid animalId)
        {
            UpdatedAnimalUser = updatedAnimalUser;
            UserId = userId;
            AnimalId = animalId;
        }

        public AnimalUserDto UpdatedAnimalUser { get; }
        public Guid UserId { get; }
        public Guid AnimalId { get; }
    }
}
