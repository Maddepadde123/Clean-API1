using Application.Commands.Users;
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
using Domain.Models.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
            var token = await _mediator.Send(new UserLoginQuery
            {
                UserName = model.Username,
                Password = model.Password
            });

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto newUser)
        {
            return Ok(await _mediator.Send(new RegisterUserCommand(newUser)));
        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _mediator.Send(new GetAllUsersQuery()));
        }

        [HttpGet("GetUserById/{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery(userId)));
        }

        [HttpPut("UpdateUserById/{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto updatedUser, Guid userId)
        {
            return Ok(await _mediator.Send(new UpdateUserByIdCommand(updatedUser, userId)));
        }

        [HttpDelete("DeleteUserById/{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            return Ok(await _mediator.Send(new DeleteUserByIdCommand(userId)));
        }

        [HttpPost("addNewConnectionAnimalUser")]
        public async Task<IActionResult> AddAnimalUser([FromBody] AnimalUserDto newAnimalUser)
        {
            try
            {
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
                // Logga och hantera fel här
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetAllAnimalUsers")]
        public async Task<IActionResult> GetAllAnimalUsers()
        {
            return Ok(await _mediator.Send(new GetAllAnimalUsersQuery()));
        }

        [HttpGet("GetAnimalUserById/{userId}/{animalId}")]
        public async Task<IActionResult> GetAnimalUserById(Guid userId, Guid animalId)
        {
            return Ok(await _mediator.Send(new GetAnimalUserByIdQuery(userId, animalId)));
        }

        [HttpPut("UpdateAnimalUserById/{userId}/{animalId}")]
        public async Task<IActionResult> UpdateAnimalUser([FromBody] AnimalUserDto updatedAnimalUser, Guid userId, Guid animalId)
        {
            return Ok(await _mediator.Send(new UpdateAnimalUserByIdCommand(updatedAnimalUser, userId, animalId)));
        }

        [HttpDelete("DeleteAnimalUserById/{userId}/{animalId}")]
        public async Task<IActionResult> DeleteAnimalUser(Guid userId, Guid animalId)
        {
            return Ok(await _mediator.Send(new DeleteAnimalUserByIdCommand(userId, animalId)));
        }
    }

}

