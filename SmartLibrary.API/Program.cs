using SmartLibrary.API.Filters;
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
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            {
                //app.UseMiddleware<ErrorHandlingMiddleware>();
                app.UseExceptionHandler("/error");
                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();
                app.Run();
            }
        }
    }
}
