using Application.Commands.Birds;
using Application.Commands.Birds.DeleteDog;
using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Application.Queries.Birds.GetAll;
using Application.Queries.Birds.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.BirdsController
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirdsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        public BirdsController(IMediator mediator) //Konstruktorn som tar emot en instans av IMediator som en parameter och används för att injecta en instans av IMediator när instansen av BirdController skapas.
        {
            _mediator = mediator;
        }

        // Get all birds from database
        [HttpGet]
        [Route("getAllBirds")]
        public async Task<IActionResult> GetAllBirds() //Metod för att presentera resultatet av getAllBirds. 
        {
            return Ok(await _mediator.Send(new GetAllBirdsQuery()));
            //return Ok("GET ALL BIRDS");
        }

        // Get a bird by Id
        [HttpGet]
        [Route("getBirdById/{birdId}")]
        public async Task<IActionResult> GetBirdById(Guid birdId)
        {
            return Ok(await _mediator.Send(new GetBirdByIdQuery(birdId)));
            //return Ok("GET BIRDS BY ID");
        }

        // Create a new bird 
        [HttpPost]
        [Route("addNewBird")]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird)
        {
            return Ok(await _mediator.Send(new AddBirdCommand(newBird)));
            //return Ok("CREATE NEW BIRD");
        }

        // Update a specific bird
        [HttpPut]
        [Route("updateBird/{updatedBirdId}")]
        public async Task<IActionResult> UpdateBird([FromBody] BirdDto updatedBird, Guid updatedBirdId)
        {
            return Ok(await _mediator.Send(new UpdateBirdByIdCommand(updatedBird, updatedBirdId)));
        }

        [HttpDelete] //En HTTPförfrågan som svarar på HttpDelete.
        [Route("deleteBird/{deletedBirdId}")] //URL-koden som syns på swagger.

        //Metoden som utför själva Httpförfrågan och Task<IActionResult> innebär att den utför en delete operation som retunerar en Http-respons. MediatR används för att utföra kommandot och skapar en HTTP-200 respons om borttagningen lyckades.
        public async Task<IActionResult> DeleteBird(Guid deletedBirdId)
        {
            return Ok(await _mediator.Send(new DeleteBirdByIdCommand(deletedBirdId)));
        }

    }
}
