using SmartLibrary.Domain.Entities;

namespace SmartLibrary.Application.Services.Authentication
{
    public record AuthenticationResult(
        User User,
        string Token
    );
}
