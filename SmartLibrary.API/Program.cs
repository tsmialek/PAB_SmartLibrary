using GraphQL.Types;
using GraphQL;
using SmartLibrary.API.GraphQL;
using SmartLibrary.API.Middleware;
using SmartLibrary.Application;
using SmartLibrary.Infrastructure;

namespace SmartLibrary.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            {
                builder.Services
                    .AddApplication()
                    .AddInfrastructure(builder.Configuration);
                //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                // GraphQL
                builder.Services.AddScoped<BookType>();
                builder.Services.AddScoped<SmartLibraryQuery>();
                builder.Services.AddScoped<ISchema, SmartLibrarySchema>();
                builder.Services.AddGraphQL(options =>
                {
                    options.AddSystemTextJson();
                    options.AddErrorInfoProvider(opt => opt.ExposeExceptionDetails = true);
                });
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseGraphQLPlayground();
            }

            {
                app.UseExceptionHandler("/error");
                app.UseHttpsRedirection();
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseMiddleware<SecurityHeadersMiddleware>();

                // GraphQL
                app.UseGraphQL<ISchema>();

                app.MapControllers();
                app.Run();
            }
        }
    }
}
