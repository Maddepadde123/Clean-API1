using Application.Dtos;  // Importera nödvändig namespace för DogDto
using Domain.Models;  // Importera nödvändig namespace för Dog-klassen
using MediatR;  // Importera nödvändig namespace för MediatR

namespace Application.Commands.Dogs
{
    public class AddDogCommand : IRequest<Dog>
    {
        public AddDogCommand(DogDto newDog)  // Deklarera konstruktorn för AddDogCommand och acceptera en instans av DogDto som en parameter
        {
            NewDog = newDog;  // Tilldela det nya hundobjektet från kommandot till NewDog-egenskapen
        }

        public DogDto NewDog { get; }  // Deklarera en egenskap för att lagra det nya hundobjektet som ska skapas
    }
}
