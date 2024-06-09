namespace SmartLibrary.Contracts.Authentication
{
    public record UserResponse(
        Guid Id,
        string FirstName, 
        string LastName,
        string Email,
        string Password,
        ICollection<Role> roles
        );

    public record Role(
        Guid Id, 
        string Name
        );
}
