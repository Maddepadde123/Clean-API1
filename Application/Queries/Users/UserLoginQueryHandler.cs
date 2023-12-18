using Application.Queries.Users;
using Infrastructure.Authentication;
using MediatR;

namespace Application.Handlers.Users
{
    public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, string>
    {
        private readonly Authentication _authentication;

        public UserLoginQueryHandler(Authentication authentication)
        {
            _authentication = authentication;
        }

        public Task<string> Handle(UserLoginQuery request, CancellationToken cancellationToken)
        {
            var user = _authentication.AuthenticateUser(request.UserName, request.Password);
            // Här kan du hantera autentisering och JWT-generering med användardata från request.LoginUser.Username och request.LoginUser.Password

            //// Exempel:
            //var username = request.LoginUser.Username;
            //var password = request.LoginUser.Password;

            // Utför autentisering av användaren här...

            //Om autentiseringen lyckas, generera JWT-token
            //var token = GenerateJwtToken(UserName);

            // Returnera JWT-token (eller annan relevant data) tillbaka
            var token = _authentication.GenerateJwtToken(user);
            return Task.FromResult(token);
        }

        //private string GenerateJwtToken(string username)
        //{
        //    // Här kan du implementera logiken för att generera JWT-token baserat på användarnamnet (eller annan data)

        //    // Detta är en stubb - Ersätt med din faktiska JWT-genereringslogik
        //    var token = "ExempelJWTTokenFör" + username;

        //    return token;
        //}
    }
}