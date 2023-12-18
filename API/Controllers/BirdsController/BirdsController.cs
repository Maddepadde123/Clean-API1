using Application.Commands.Birds;  // Importera nödvändig namespace för Bird-relaterade kommandon
using Application.Commands.Birds.DeleteDog;  // Importera nödvändig namespace för Bird-relaterade kommandon för att radera
using Application.Commands.Birds.UpdateBird;  // Importera nödvändig namespace för Bird-relaterade kommandon för att uppdatera
using Application.Dtos;  // Importera nödvändig namespace för Data Transfer Objects (DTOs)
using Application.Queries.Birds.GetAll;  // Importera nödvändig namespace för Bird-relaterade frågor för att hämta alla
using Application.Queries.Birds.GetById;  // Importera nödvändig namespace för Bird-relaterade frågor för att hämta efter ID
using MediatR;  // Importera nödvändig namespace för MediatR, en medlingsbibliotek för att hantera kommandon och frågor
using Microsoft.AspNetCore.Authorization;  // Importera nödvändig namespace för Authorization-funktionalitet
using Microsoft.AspNetCore.Mvc;  // Importera nödvändig namespace för ASP.NET Core MVC-funktionalitet

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.BirdsController
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        internal readonly IMediator _mediator;  // Deklarera ett internt fält för att lagra en instans av IMediator

        public BirdsController(IMediator mediator)  // Konstruktorn som tar emot en instans av IMediator som en parameter och används för att injecta en instans av IMediator när instansen av BirdController skapas.
        {
            _mediator = mediator;  // Tilldela den injicerade IMediator-instanse till det interna fältet
        }

        // Get all birds from database
        [HttpGet]
        [Route("getAllBirds")]
        public async Task<IActionResult> GetAllBirds()  // Metod för att presentera resultatet av getAllBirds. 
        {
            return Ok(await _mediator.Send(new GetAllBirdsQuery()));  // Anropa en MediatR-fråga för att hämta alla fåglar och returnera resultatet som en HTTP 200 OK-respons
            //return Ok("GET ALL BIRDS");
        }

        // Get a bird by Id
        [HttpGet]
        [Route("getBirdById/{birdId}")]
        public async Task<IActionResult> GetBirdById(Guid birdId)
        {
            return Ok(await _mediator.Send(new GetBirdByIdQuery(birdId)));  // Anropa en MediatR-fråga för att hämta en fågel efter ID och returnera resultatet som en HTTP 200 OK-respons
            //return Ok("GET BIRDS BY ID");
        }

        // Create a new bird 
        [HttpPost]
        [Route("addNewBird")]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird)
        {
            return Ok(await _mediator.Send(new AddBirdCommand(newBird)));  // Anropa en MediatR-kommando för att lägga till en ny fågel och returnera resultatet som en HTTP 200 OK-respons
            //return Ok("CREATE NEW BIRD");
        }

        // Update a specific bird
        [HttpPut]
        [Route("updateBird/{updatedBirdId}")]
        public async Task<IActionResult> UpdateBird([FromBody] BirdDto updatedBird, Guid updatedBirdId)
        {
            return Ok(await _mediator.Send(new UpdateBirdByIdCommand(updatedBird, updatedBirdId)));  // Anropa en MediatR-kommando för att uppdatera en fågel och returnera resultatet som en HTTP 200 OK-respons
        }

        [HttpDelete]  // En HTTP-förfrågan som svarar på HttpDelete.
        [Route("deleteBird/{deletedBirdId}")]  // URL-koden som syns på swagger.

        // Metoden som utför själva HTTP-förfrågan och Task<IActionResult> innebär att den utför en delete operation som retunerar en HTTP-respons. MediatR används för att utföra kommandot och skapar en HTTP-200 respons om borttagningen lyckades.
        public async Task<IActionResult> DeleteBird(Guid deletedBirdId)
        {
            return Ok(await _mediator.Send(new DeleteBirdByIdCommand(deletedBirdId)));  // Anropa en MediatR-kommando för att ta bort en fågel och returnera resultatet som en HTTP 200 OK-respons
        }
    }
}
