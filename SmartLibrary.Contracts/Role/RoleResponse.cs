using SmartLibrary.Contracts.Authentication;

namespace SmartLibrary.Contracts.Role
{
    public record RoleResponse(
        Guid Id,
        string Name,
        ICollection<User> users
    );

    public record User(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Password
    );
}
