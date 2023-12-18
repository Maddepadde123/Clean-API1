using Application.Commands.Dogs;  // Importera nödvändig namespace för Dog-relaterade kommandon
using Application.Commands.Dogs.DeleteDog;  // Importera nödvändig namespace för Dog-relaterade kommandon för att radera
using Application.Commands.Dogs.UpdateDog;  // Importera nödvändig namespace för Dog-relaterade kommandon för att uppdatera
using Application.Dtos;  // Importera nödvändig namespace för Data Transfer Objects (DTOs)
using Application.Queries.Dogs.GetAll;  // Importera nödvändig namespace för Dog-relaterade frågor för att hämta alla
using Application.Queries.Dogs.GetById;  // Importera nödvändig namespace för Dog-relaterade frågor för att hämta efter ID
using MediatR;  // Importera nödvändig namespace för MediatR, en medlingsbibliotek för att hantera kommandon och frågor
using Microsoft.AspNetCore.Mvc;  // Importera nödvändig namespace för ASP.NET Core MVC-funktionalitet

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.DogsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        internal readonly IMediator _mediator;  // Deklarera ett internt fält för att lagra en instans av IMediator

        public DogsController(IMediator mediator)  // Konstruktorn som tar emot en instans av IMediator som en parameter och används för att injecta en instans av IMediator när instansen av DogsController skapas.
        {
            _mediator = mediator;  // Tilldela den injicerade IMediator-instanse till det interna fältet
        }

        // Get all dogs from database
        [HttpGet]
        [Route("getAllDogs")]

        public async Task<IActionResult> GetAllDogs()
        {
            return Ok(await _mediator.Send(new GetAllDogsQuery()));  // Anropa en MediatR-fråga för att hämta alla hundar och returnera resultatet som en HTTP 200 OK-respons
            //return Ok("GET ALL DOGS");
        }

        // Get a dog by Id
        [HttpGet]
        [Route("getDogById/{dogId}")]
        public async Task<IActionResult> GetDogById(Guid dogId)
        {
            return Ok(await _mediator.Send(new GetDogByIdQuery(dogId)));  // Anropa en MediatR-fråga för att hämta en hund efter ID och returnera resultatet som en HTTP 200 OK-respons
        }

        // Create a new dog 
        [HttpPost]
        [Route("addNewDog")]
        public async Task<IActionResult> AddDog([FromBody] DogDto newDog)
        {
            return Ok(await _mediator.Send(new AddDogCommand(newDog)));  // Anropa en MediatR-kommando för att lägga till en ny hund och returnera resultatet som en HTTP 200 OK-respons
        }

        // Update a specific dog
        [HttpPut]
        [Route("updateDog/{updatedDogId}")]
        public async Task<IActionResult> UpdateDog([FromBody] DogDto updatedDog, Guid updatedDogId)
        {
            return Ok(await _mediator.Send(new UpdateDogByIdCommand(updatedDog, updatedDogId)));  // Anropa en MediatR-kommando för att uppdatera en hund och returnera resultatet som en HTTP 200 OK-respons
        }

        // IMPLEMENT DELETE !!!

        [HttpDelete]
        [Route("deleteDog/{deletedDogId}")]
        public async Task<IActionResult> DeleteDog(Guid deletedDogId)
        {
            return Ok(await _mediator.Send(new DeleteDogByIdCommand(deletedDogId)));  // Anropa en MediatR-kommando för att ta bort en hund och returnera resultatet som en HTTP 200 OK-respons
        }

    }
}
