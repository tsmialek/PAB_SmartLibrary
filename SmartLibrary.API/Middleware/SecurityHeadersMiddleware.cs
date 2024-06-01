using System.Diagnostics;

namespace SmartLibrary.API.Middleware
{
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                // Dodanie nagłówków bezpieczeństwa
                context.Response.Headers["Content-Security-Policy"] = "default-src 'self'";
                context.Response.Headers["X-Content-Type-Options"] = "nosniff";
                context.Response.Headers["X-Frame-Options"] = "DENY";
                context.Response.Headers["X-XSS-Protection"] = "1; mode=block";

                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
