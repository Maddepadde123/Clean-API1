using Application.Commands.Birds;
using Application.Commands.Birds.DeleteDog;
using Application.Commands.Birds.UpdateBird;
using Application.Dtos;
using Application.Queries.Birds.GetAll;
using Application.Queries.Birds.GetById;
using Application.Validators;
using FluentValidation.Results;  // Importera FluentValidation.Results
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.BirdsController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BirdsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BirdsController> _logger;

        public BirdsController(IMediator mediator, ILogger<BirdsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("getAllBirds")]
        public async Task<IActionResult> GetAllBirds()
        {
            try
            {
                var birds = await _mediator.Send(new GetAllBirdsQuery());
                var sortedBirds = birds.OrderByDescending(b => b.Name).ToList();
                return Ok(sortedBirds);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllBirds: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("getBirdById/{birdId}")]
        [Authorize]
        public async Task<IActionResult> GetBirdById(Guid birdId)
        {
            try
            {
                return Ok(await _mediator.Send(new GetBirdByIdQuery(birdId)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetBirdById: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("addNewBird")]
        public async Task<IActionResult> AddBird([FromBody] BirdDto newBird)
        {
            try
            {
                // Använd BirdValidator för att validera newBird
                var validator = new BirdValidator();
                ValidationResult validationResult = await validator.ValidateAsync(newBird);

                // Om valideringen misslyckas returneras en BadRequest med felmeddelanden
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
                }

                // Om valideringen lyckas, fortsätt med att skicka kommandot till MediatR
                return Ok(await _mediator.Send(new AddBirdCommand(newBird)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AddBird: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [Route("updateBird/{updatedBirdId}")]
        public async Task<IActionResult> UpdateBird([FromBody] BirdDto updatedBird, Guid updatedBirdId)
        {
            try
            {
                // Använd BirdValidator för att validera updatedBird
                var validator = new BirdValidator();
                ValidationResult validationResult = await validator.ValidateAsync(updatedBird);

                // Om valideringen misslyckas returneras en BadRequest med felmeddelanden
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
                }

                // Om valideringen lyckas, fortsätt med att skicka kommandot till MediatR
                return Ok(await _mediator.Send(new UpdateBirdByIdCommand(updatedBird, updatedBirdId)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateBird: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete]
        [Route("deleteBird/{deletedBirdId}")]
        public async Task<IActionResult> DeleteBird(Guid deletedBirdId)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteBirdByIdCommand(deletedBirdId)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteBird: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
