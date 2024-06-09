using FluentAssertions;
using Moq;
using SmartLibrary.Application.Common.Error.Authentication;
using SmartLibrary.Application.Common.Interfaces.Authentication;
using SmartLibrary.Application.Common.Interfaces.Persistance;
using SmartLibrary.Application.Services.Authentication;
using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Tests.Services
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IRoleRepository> _roleRepositoryMock;
        private readonly AuthenticationService _authenticationService;

        public AuthenticationServiceTests()
        {
            _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _roleRepositoryMock = new Mock<IRoleRepository>();
            _authenticationService = new AuthenticationService(_jwtTokenGeneratorMock.Object, _userRepositoryMock.Object, _roleRepositoryMock.Object);
        }

        [Fact]
        public void Register_WithExistingEmail_ShouldThrowDuplicateEmailException()
        {
            // Arrange
            var email = "test@example.com";
            var existingUser = new User { Email = email };
            _userRepositoryMock.Setup(repo => repo.GetByEmail(email)).Returns(existingUser);

            // Act
            var act = () => _authenticationService.Register("FirstName", "LastName", email, "Password");

            // Assert
            act.Should().Throw<DuplicateEmailException>();  
        }

        [Fact]
        public void Register_WithNewEmail_ShouldReturnAuthenticationResult()
        {
            // Arrange 
            var email = "newUser@example.com";
            _userRepositoryMock.Setup(repo => repo.GetByEmail(email)).Returns((User)null);
            var newUser = new User { Email = email };
            _userRepositoryMock.Setup(repo => repo.Add(It.IsAny<User>())).Callback<User>(user => newUser = user);
            var token = "jwt_token";
            _jwtTokenGeneratorMock.Setup(jwt => jwt.GenerateToken(It.IsAny<User>())).Returns(token);

            // Act
            var result = _authenticationService.Register("FirstName", "LastName", email, "Password");

            // Assert 
            result.Should().NotBeNull();
            result.Token.Should().Be(token);
            result.User.Should().Be(newUser);
        }

        [Fact]
        public void Login_WithInvalidEmail_ShouldThrowNonExistingUserException()
        {
            // Arrange
            var email = "nonExistingUser@example.com";
            _userRepositoryMock.Setup(repo => repo.GetByEmail(email)).Returns((User)null);

            // Act
            var result = () => _authenticationService.Login(email, "Password");

            // Assert
            result.Should().Throw<NonExistingUserException>();
        }

        [Fact]
        public void Login_WithInvalidPassword_ShoudThrowInvalidCredentialsException()
        {
            // Arrange
            var email = "user@example.com";
            var user = new User { Email = email, Password = "correctPassword" };
            _userRepositoryMock.Setup(repo => repo.GetByEmail(email)).Returns(user);

            // Act
            var result = () => _authenticationService.Login(email, "Password");

            // Assert
            result.Should().Throw<InvalidCredentialsException>();
        }

        [Fact]
        public void Login_WithValidCredentials_ShouldReturnAuthenticationResult()
        {
            // Arrange 
            var email = "user@example.com";
            var password = "correctPassword";
            var user = new User { Email = email, Password = password };
            _userRepositoryMock.Setup(repo => repo.GetByEmail(email)).Returns(user);
            var token = "jwt_token";
            _jwtTokenGeneratorMock.Setup(jwt => jwt.GenerateToken(user)).Returns(token);

            // Act
            var result = _authenticationService.Login(email, password);

            // Assert
            result.Should().NotBeNull();
            result.User.Should().Be(user);
            result.Token.Should().Be(token);
        }
    }
}
