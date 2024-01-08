using Application.Commands.Cats;
using Application.Commands.Cats.DeleteDog;
using Application.Commands.Cats.UpdateCat;
using Application.Dtos;
using Application.Queries.Cats.GetAll;
using Application.Queries.Cats.GetById;
using Application.Queries.Cats.GetCatsByWeightAndBreed;
using Application.Validators;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CatsController
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CatsController : ControllerBase
    {
        internal readonly IMediator _mediator;
        private readonly ILogger<CatsController> _logger;

        public CatsController(IMediator mediator, ILogger<CatsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("getAllCats")]
        public async Task<IActionResult> GetAllCats()
        {
            try
            {
                var cats = await _mediator.Send(new GetAllCatsQuery());
                var sortedCats = cats.OrderByDescending(c => c.Name).ToList();
                return Ok(sortedCats);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetAllCats: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("getCatById/{catId}")]
        [Authorize]
        public async Task<IActionResult> GetCatById(Guid catId)
        {
            try
            {
                return Ok(await _mediator.Send(new GetCatByIdQuery(catId)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCatById: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost]
        [Route("addNewCat")]
        public async Task<IActionResult> AddCat([FromBody] CatDto newCat)
        {
            try
            {
                var validator = new CatValidator();
                ValidationResult validationResult = await validator.ValidateAsync(newCat);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
                }

                return Ok(await _mediator.Send(new AddCatCommand(newCat)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in AddCat: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [Route("updateCat/{updatedCatId}")]
        public async Task<IActionResult> UpdateCat([FromBody] CatDto updatedCat, Guid updatedCatId)
        {
            try
            {
                var validator = new CatValidator();
                ValidationResult validationResult = await validator.ValidateAsync(updatedCat);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.Select(error => error.ErrorMessage));
                }

                return Ok(await _mediator.Send(new UpdateCatByIdCommand(updatedCat, updatedCatId)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateCat: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete]
        [Route("deleteCat/{deletedCatId}")]
        public async Task<IActionResult> DeleteCat(Guid deletedCatId)
        {
            try
            {
                return Ok(await _mediator.Send(new DeleteCatByIdCommand(deletedCatId)));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteCat: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet]
        [Route("getCatsByWeightAndBreed")]
        public async Task<IActionResult> GetCatsByWeightAndBreed([FromQuery] string breed, [FromQuery] int? weight)
        {
            try
            {
                return Ok(await _mediator.Send(new GetCatsByWeightAndBreedQuery { CatBreed = breed, CatWeight = weight }));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in GetCatsByWeightAndBreed: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
