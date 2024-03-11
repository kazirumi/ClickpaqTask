using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DotNetApi.Data;
using DotNetApi.Model;

namespace DotNetApi.AuthServices
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context,IJwtUtils jwtUtils,ApplicationDBContext applicationDBContext)
        {
            string path = context.Request.Path.Value;
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                if (IsEnableUnauthorizedRoute(path))
                {
                    await _next(context);
                    return;
                }
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            else
            {
                var IsUserID = Guid.TryParse(jwtUtils.ValidateJwtToken(token),out Guid userId);
                if (userId != null && IsUserID)
                {
                    //attach user to context on successful jwt validation
                    context.Items["User"] = await applicationDBContext.users.FirstOrDefaultAsync(x=>x.Id==userId);
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }
            }



            await _next(context);
        }

        private bool IsEnableUnauthorizedRoute(string path)
        {
            List<string> UnauthorizedRoutes = new List<string>
            {
                "/api/Authentication/Login",
                "/api/Authentication/Register"
            };

            if (UnauthorizedRoutes.Contains(path))
                return true;

            return false;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
