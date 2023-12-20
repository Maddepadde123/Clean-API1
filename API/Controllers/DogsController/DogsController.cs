using Application.Commands.Dogs;
using Application.Commands.Dogs.DeleteDog;
using Application.Commands.Dogs.UpdateDog;
using Application.Dtos;
using Application.Queries.Dogs.GetAll;
using Application.Queries.Dogs.GetById;
using Application.Validators;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.DogsController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DogsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        private readonly ILogger<DogsController> _logger;

        public DogsController(IMediator mediator, ILogger<DogsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("getAllDogs")]
        public async Task<IActionResult> GetAllDogs()
        {
            try
            {
                var dogs = await _mediator.Send(new GetAllDogsQuery());
                var sortedDogs = dogs.OrderByDescending(d => d.Name).ToList();
                return Ok(sortedDogs);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllDogs: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("getDogById/{dogId}")]

        public async Task<IActionResult> GetDogById(Guid dogId)
        {
            try
            {
                return Ok(await _mediator.Send(new GetDogByIdQuery(dogId)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetDogById: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("addNewDog")]
        public async Task<IActionResult> AddDog([FromBody] DogDto newDog)
        {
            try
            {
                var validator = new DogValidator();
                ValidationResult validationResult = await validator.ValidateAsync(newDog);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
                }

                return Ok(await _mediator.Send(new AddDogCommand(newDog)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AddDog: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [Route("updateDog/{updatedDogId}")]
        public async Task<IActionResult> UpdateDog([FromBody] DogDto updatedDog, Guid updatedDogId)
        {
            try
            {
                var validator = new DogValidator();
                ValidationResult validationResult = await validator.ValidateAsync(updatedDog);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
                }

                return Ok(await _mediator.Send(new UpdateDogByIdCommand(updatedDog, updatedDogId)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateDog: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete]
        [Route("deleteDog/{deletedDogId}")]
        public async Task<IActionResult> DeleteDog(Guid deletedDogId)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteDogByIdCommand(deletedDogId)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteDog: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
