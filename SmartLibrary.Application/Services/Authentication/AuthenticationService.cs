using SmartLibrary.Application.Common.Error;
using SmartLibrary.Application.Common.Interfaces.Authentication;
using SmartLibrary.Application.Common.Interfaces.Persistance;
using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Login(string email, string password)
        {
            // 1. Validate user exists
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new NonExistingUserException();
            }

            // 2. Validate password
            if (user.Password != password)
            {
                throw new Exception("Invalid password");
            }

            // 3. Create JWT token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            // 1. Validate the user doesn't exist
            if (_userRepository.GetUserByEmail(email) is not null)
            {
                throw new DuplicateEmailException();
            }

            // 2. Create user (generate unique id)
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            // 4. Add role to user
            var userRole = _roleRepository.GetByName("User");
            if (userRole is not null)
            {
                user.Roles.Add(userRole);
            }

            _userRepository.Add(user);

            // 3. Generate token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
