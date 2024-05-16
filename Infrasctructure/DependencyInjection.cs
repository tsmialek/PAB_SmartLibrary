using Microsoft.Extensions.DependencyInjection;
using SmartLibrary.Application.Common.Interfaces.Authentication;
using SmartLibrary.Application.Common.Interfaces.Persistance;
using SmartLibrary.Application.Common.Interfaces.Services;
using SmartLibrary.Infrastructure.Authentication;
using SmartLibrary.Infrastructure.Persistance;
using SmartLibrary.Infrastructure.Services;

namespace SmartLibrary.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            Microsoft.Extensions.Configuration.ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
