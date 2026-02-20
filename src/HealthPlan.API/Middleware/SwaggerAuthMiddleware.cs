using System.Net;
using System.Text;

namespace HealthPlan.API.Middleware
{
    public class SwaggerAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private const string SwaggerPath = "/swagger";
        private const string IndexPath = "/index.html";
        private const string RootPath = "/";
        private readonly string _username;
        private readonly string _password;

        public SwaggerAuthMiddleware(RequestDelegate next)
        {
            _next = next;
            _username = Environment.GetEnvironmentVariable("SWAGGER_USERNAME")
                ?? throw new InvalidOperationException("SWAGGER_USERNAME environment variable is not set.");
            _password = Environment.GetEnvironmentVariable("SWAGGER_PASSWORD")
                ?? throw new InvalidOperationException("SWAGGER_PASSWORD environment variable is not set.");
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments(SwaggerPath) ||
                context.Request.Path.StartsWithSegments(IndexPath) ||
                context.Request.Path == RootPath)
            {
                string? authHeader = context.Request.Headers["Authorization"];
               
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    var encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
                    var parts = decodedUsernamePassword.Split(':', 2);
                    if (parts.Length == 2 && parts[0] == _username && parts[1] == _password)
                    {
                        await _next(context);
                        return;
                    }
                }

                context.Response.Headers["WWW-Authenticate"] = "Basic";
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            await _next(context);
        }
    }
}