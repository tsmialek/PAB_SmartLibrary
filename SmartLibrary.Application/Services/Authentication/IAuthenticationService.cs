namespace SmartLibrary.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
         AuthenticationResult Login(string Email, string password);
         AuthenticationResult Register(string firstName, string lastName, string email, string password);
    }
}
