using Application.Commands.Users.AddAnimalUser;
using Application.Commands.Users.DeleteAnimalUser;
using Application.Commands.Users.DeleteUser;
using Application.Commands.Users.RegisterUser;
using Application.Commands.Users.UpdateAnimalUser;
using Application.Commands.Users.UpdateUser;
using Application.Dtos;
using Application.Queries.Users;
using Application.Queries.Users.GetAll;
using Application.Queries.Users.GetAllAnimalUsers;
using Application.Queries.Users.GetAnimalUserById;
using Application.Queries.Users.GetById;
using Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;

        public UserController(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequests model)
        {
            try
            {
                var token = await _mediator.Send(new UserLoginQuery
                {
                    UserName = model.Username,
                    Password = model.Password
                });

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                // Logga fel och returnera lämpligt svar
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto newUser)
        {
            try
            {
                // Validera UserRegistrationDto
                if (string.IsNullOrWhiteSpace(newUser.Username))
                {
                    return BadRequest("Username is required.");
                }

                if (string.IsNullOrWhiteSpace(newUser.Password))
                {
                    return BadRequest("Password is required.");
                }

                var result = await _mediator.Send(new RegisterUserCommand(newUser));

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Logga fel och returnera lämpligt svar
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _mediator.Send(new GetAllUsersQuery());
                return Ok(users);
            }
            catch (Exception ex)
            {
                // Logga fel och returnera lämpligt svar
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetUserById/{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            try
            {
                var user = await _mediator.Send(new GetUserByIdQuery(userId));

                if (user == null)
                {
                    return NotFound("User not found");
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                // Logga fel och returnera lämpligt svar
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateUserById/{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto updatedUser, Guid userId)
        {
            try
            {
                // Validera UserDto
                if (updatedUser == null)
                {
                    return BadRequest("UserDto is required.");
                }

                var result = await _mediator.Send(new UpdateUserByIdCommand(updatedUser, userId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Logga fel och returnera lämpligt svar
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteUserById/{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteUserByIdCommand(userId));

                if (result == null)
                {
                    return NotFound("User not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Logga fel och returnera lämpligt svar
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("addNewConnectionAnimalUser")]
        public async Task<IActionResult> AddAnimalUser([FromBody] AnimalUserDto newAnimalUser)
        {
            try
            {
                // Validera AnimalUserDto
                if (newAnimalUser == null)
                {
                    return BadRequest("AnimalUserDto is required.");
                }

                if (newAnimalUser.UserId == Guid.Empty)
                {
                    return BadRequest("UserId is required.");
                }

                if (newAnimalUser.AnimalId == Guid.Empty)
                {
                    return BadRequest("AnimalId is required.");
                }

                var result = await _mediator.Send(new AddAnimalUserCommand(newAnimalUser));

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Failed to create the Animal-User connection.");
                }
            }
            catch (Exception ex)
            {
                // Logga fel och returnera lämpligt svar
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllAnimalUsers")]
        public async Task<IActionResult> GetAllAnimalUsers()
        {
            try
            {
                var animalUsers = await _mediator.Send(new GetAllAnimalUsersQuery());
                return Ok(animalUsers);
            }
            catch (Exception ex)
            {
                // Logga fel och returnera lämpligt svar
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAnimalUserById/{userId}/{animalId}")]
        public async Task<IActionResult> GetAnimalUserById(Guid userId, Guid animalId)
        {
            try
            {
                var animalUser = await _mediator.Send(new GetAnimalUserByIdQuery(userId, animalId));

                if (animalUser == null)
                {
                    return NotFound("Animal-User connection not found");
                }

                return Ok(animalUser);
            }
            catch (Exception ex)
            {
                // Logga fel och returnera lämpligt svar
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateAnimalUserById/{userId}/{animalId}")]
        public async Task<IActionResult> UpdateAnimalUser([FromBody] AnimalUserDto updatedAnimalUser, Guid userId, Guid animalId)
        {
            try
            {
                // Validera AnimalUserDto
                if (updatedAnimalUser == null)
                {
                    return BadRequest("AnimalUserDto is required.");
                }

                if (updatedAnimalUser.UserId != userId || updatedAnimalUser.AnimalId != animalId)
                {
                    return BadRequest("Mismatch between the provided IDs and the IDs in the request body.");
                }

                var result = await _mediator.Send(new UpdateAnimalUserByIdCommand(updatedAnimalUser, userId, animalId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Logga fel och returnera lämpligt svar
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteAnimalUserById/{userId}/{animalId}")]
        public async Task<IActionResult> DeleteAnimalUser(Guid userId, Guid animalId)
        {
            try
            {
                var result = await _mediator.Send(new DeleteAnimalUserByIdCommand(userId, animalId));

                if (result == null)
                {
                    return NotFound("Animal-User connection not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Logga fel och returnera lämpligt svar
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
