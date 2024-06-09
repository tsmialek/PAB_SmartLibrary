using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLibrary.Application.Services.Authentication;
using SmartLibrary.Contracts.Authentication;
using SmartLibrary.Contracts.Role;

namespace SmartLibrary.API.Controllers
{
    [Route("auth")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(Contracts.Authentication.RegisterRequest request)
        {
            var authResult = _authenticationService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            var response = new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(Contracts.Authentication.LoginRequest request)
        {
            var authResult = _authenticationService.Login(
                request.Email,
                request.Password);

            var response = new AuthenticationResponse(
                authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token);

            return Ok(response);
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            var users = _authenticationService.GetAll();

            var response = new List<UserResponse>();

            foreach(var user in users)
            {
                var roles = user.Roles.Select(r => new SmartLibrary.Contracts.Authentication.Role(r.Id, r.Name)).ToList();

                response.Add(new UserResponse(
                    user.Id,
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.Password,
                    roles));
            }


            return Ok(response);
        }

        [HttpGet("id/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetById(Guid id)
        {
            var user = _authenticationService.GetById(id);

            var roles = user.Roles.Select(r => new SmartLibrary.Contracts.Authentication.Role(r.Id, r.Name)).ToList();

            var response = new UserResponse(
                user.Id,
                user.FirstName,
                user.LastName, 
                user.Email,
                user.Password,
                roles
                );

            return Ok(response);
        }

        [HttpGet("email/{email}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetByEmail(string email)
        {
            var user = _authenticationService.GetByEmail(email);

            var roles = user.Roles.Select(r => new SmartLibrary.Contracts.Authentication.Role(r.Id, r.Name)).ToList();

            var response = new UserResponse(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Password,
                roles
                );

            return Ok(response);
        }
    }
}
