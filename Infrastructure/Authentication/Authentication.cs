using Domain.Models.User;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Authentication
{
    public class Authentication
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public Authentication(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public UserModel AuthenticateUser(string username, string password)
        {
            // Replace with your actual authentication logic
            var user = _userRepository.GetUserByUsernameAndPassword(username, password);
            return user;
        }
        public string GenerateJwtToken(UserModel user)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(_configuration["JwtSettings:SecretKey"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                if (token != null)
                {
                    return tokenHandler.WriteToken(token);
                }
                else
                {
                    // Handle case where token creation failed
                    return "Token creation failed.";
                }
            }
            catch (Exception ex)
            {
                // Handle exception (log it, throw it further, etc.)
                return $"Token generation failed. Exception: {ex.Message}";
            }
        }
    }
}
