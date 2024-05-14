using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Application.Services.Authentication;

namespace SmartLibrary.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        }
    }
}
