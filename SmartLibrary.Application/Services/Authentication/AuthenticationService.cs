using SmartLibrary.Application.Common.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public AuthenticationResult Login(string email, string password)
        {
            return new AuthenticationResult(
                Guid.NewGuid(),
                "firstName",
                "lastName",
                email,
                "token");
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            // Check if user exists in the database

            // Create user (generate unique id)

            // Generate token
            Guid userId = Guid.NewGuid();
            var token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);

            return new AuthenticationResult(
                Guid.NewGuid(),
                firstName,
                lastName,
                email,
                token);
        }
    }
}
