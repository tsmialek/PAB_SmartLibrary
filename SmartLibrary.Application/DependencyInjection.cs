using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Application.Services.Authentication;
using SmartLibrary.Application.Services.BookMenagement;

namespace SmartLibrary.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IBookService, BookService>();
            return services;
        }
    }
}
