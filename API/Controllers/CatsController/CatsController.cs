using Application.Commands.Cats;  // Importera nödvändig namespace för Cat-relaterade kommandon
using Application.Commands.Cats.DeleteDog;  // Importera nödvändig namespace för Cat-relaterade kommandon för att radera (Notera: Det kan vara en felaktig import. Kanske borde vara Application.Commands.Cats.DeleteCat)
using Application.Commands.Cats.UpdateCat;  // Importera nödvändig namespace för Cat-relaterade kommandon för att uppdatera
using Application.Dtos;  // Importera nödvändig namespace för Data Transfer Objects (DTOs)
using Application.Queries.Cats.GetAll;  // Importera nödvändig namespace för Cat-relaterade frågor för att hämta alla
using Application.Queries.Cats.GetById;  // Importera nödvändig namespace för Cat-relaterade frågor för att hämta efter ID
using MediatR;  // Importera nödvändig namespace för MediatR, en medlingsbibliotek för att hantera kommandon och frågor
using Microsoft.AspNetCore.Mvc;  // Importera nödvändig namespace för ASP.NET Core MVC-funktionalitet

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.CatsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        internal readonly IMediator _mediator;  // Deklarera ett internt fält för att lagra en instans av IMediator

        public CatsController(IMediator mediator)  // Konstruktorn som tar emot en instans av IMediator som en parameter och används för att injecta en instans av IMediator när instansen av CatsController skapas.
        {
            _mediator = mediator;  // Tilldela den injicerade IMediator-instanse till det interna fältet
        }

        // Get all cats from database
        [HttpGet]
        [Route("getAllCats")]
        public async Task<IActionResult> GetAllCats()
        {
            return Ok(await _mediator.Send(new GetAllCatsQuery()));  // Anropa en MediatR-fråga för att hämta alla katter och returnera resultatet som en HTTP 200 OK-respons
            //return Ok("GET ALL DOGS");
        }

        // Get a cat by Id
        [HttpGet]
        [Route("getCatById/{catId}")]
        public async Task<IActionResult> GetCatById(Guid catId)
        {
            return Ok(await _mediator.Send(new GetCatByIdQuery(catId)));  // Anropa en MediatR-fråga för att hämta en katt efter ID och returnera resultatet som en HTTP 200 OK-respons
        }

        // Create a new cat 
        [HttpPost]
        [Route("addNewCat")]
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat)
        {
            return Ok(await _mediator.Send(new AddCatCommand(newCat)));  // Anropa en MediatR-kommando för att lägga till en ny katt och returnera resultatet som en HTTP 200 OK-respons
        }

        // Update a specific Cat
        [HttpPut]
        [Route("updateCat/{updatedCatId}")]
        public async Task<IActionResult> UpdateCat([FromBody] CatDto updatedCat, Guid updatedCatId)
        {
            return Ok(await _mediator.Send(new UpdateCatByIdCommand(updatedCat, updatedCatId)));  // Anropa en MediatR-kommando för att uppdatera en katt och returnera resultatet som en HTTP 200 OK-respons
        }

        // IMPLEMENT DELETE !!!

        [HttpDelete]
        [Route("deleteCat/{deletedCatId}")]
        public async Task<IActionResult> DeleteCat(Guid deletedCatId)
        {
            return Ok(await _mediator.Send(new DeleteCatByIdCommand(deletedCatId)));  // Anropa en MediatR-kommando för att ta bort en katt och returnera resultatet som en HTTP 200 OK-respons
        }

    }
}
