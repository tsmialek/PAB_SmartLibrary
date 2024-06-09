using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Application.Services.Authentication;
using SmartLibrary.Application.Services.BookMenagement;
using SmartLibrary.Application.Services.RoleServices;

namespace SmartLibrary.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IRoleService, RoleService>();
            return services;
        }
    }
}
