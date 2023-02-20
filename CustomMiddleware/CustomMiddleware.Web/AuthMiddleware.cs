using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace MiddlewareWeb
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            var username = context.Request.Query["username"].ToString();
            var password = context.Request.Query["password"].ToString();

            if (username != "user1" || password != "password1")
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Not authorized.");
                return;
            }

            await _next(context);
        }
    }

    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuth(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}