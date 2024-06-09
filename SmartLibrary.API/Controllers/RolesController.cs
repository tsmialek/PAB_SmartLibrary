using GraphQL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartLibrary.Application.Services.RoleServices;
using SmartLibrary.Contracts.Authentication;
using SmartLibrary.Contracts.Role;
using System.Diagnostics;

namespace SmartLibrary.API.Controllers
{
    [Route("roles")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _roleService.GetAll();

            var response = new List<RoleResponse>();

            foreach (var role in roles)
            {
                var users = role.Users.Select(u => new SmartLibrary.Contracts.Role.User(
                    u.Id,
                    u.FirstName,
                    u.LastName,
                    u.Email,
                    u.Password)).ToList();

                response.Add(new RoleResponse(role.Id, role.Name, users));
            }

            return Ok(response);
        }

        [HttpPost("addUserRoleByEmail")]
        public IActionResult AddUserRole(AddUserRoleByEmailAndNameRequest request)
        {
            var user = _roleService.AddUserRole(request.UserEmail, request.NewUserRoleName);

            var roles = user.Roles.Select(r => new SmartLibrary.Contracts.Authentication.Role(r.Id, r.Name)).ToList();

            var response = new SmartLibrary.Contracts.Authentication.UserResponse(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Password, 
                roles);

            return Ok(response);
        }

        [HttpPost("addUserRoleById")]
        public IActionResult AddUserRole(AddUserRoleByIdRequest request)
        {
            var user = _roleService.AddUserRole(request.UserId, request.NewUserRoleId);

            var roles = user.Roles.Select(r => new SmartLibrary.Contracts.Authentication.Role(r.Id, r.Name)).ToList();

            var response = new SmartLibrary.Contracts.Authentication.UserResponse(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                user.Password,
                roles);

            return Ok(response);
        }
    }
}
