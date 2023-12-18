// Använder nödvändiga namnrymder och importerar klasser som behövs för koden
using Application.Dtos;      // Importerar namnrymden för Data Transfer Objects (DTOs)
using Domain.Models.User;    // Importerar namnrymden för användarmodeller
using MediatR;               // Importerar namnrymden för MediatR för att stödja Mediator pattern

// Placerar koden i namnrymden "Application.Commands.Users.RegisterUser"
namespace Application.Commands.Users.RegisterUser
{
    // Skapar en klass "RegisterUserCommand" som är en implementation av IRequest<T>
    // där T är typen av svar som förväntas från MediatR, i det här fallet en UserModel
    public class RegisterUserCommand : IRequest<UserModel>
    {
        // Konstruktor för att skapa ett nytt RegisterUserCommand-objekt med en UserRegistrationDto som parameter
        public RegisterUserCommand(UserRegistrationDto newUser)
        {
            // Tilldelar den inkommande UserRegistrationDto till den publika egenskapen NewUser
            NewUser = newUser;
        }

        // Publik egenskap (property) för att få åtkomst till den inkommande UserRegistrationDto
        public UserRegistrationDto NewUser { get; }
    }
}
