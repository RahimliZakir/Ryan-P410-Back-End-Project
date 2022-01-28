using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RyanP410.WebUI.AppCode.Extensions;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using System.Threading.Tasks;

namespace RyanP410.WebUI.AppCode.Middlewares
{
    public class AuditMiddleware
    {
        private readonly RequestDelegate _next;

        public AuditMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            using (IServiceScope scope = httpContext.RequestServices.CreateScope())
            {
                RyanDbContext db = scope.ServiceProvider.GetRequiredService<RyanDbContext>();

                AuditLog log = new AuditLog
                {
                    RequestTime = DateTime.UtcNow.AddHours(4)
                };

                log.Path = httpContext.Request.Path;
                log.IsHttps = httpContext.Request.IsHttps;
                log.QueryString = httpContext.Request.QueryString.ToString();
                log.Method = httpContext.Request.Method;

                RouteData routeData = httpContext.GetRouteData();

                if (routeData.Values.TryGetValue("area", out object area))
                    log.Area = area.ToString();

                if (routeData.Values.TryGetValue("controller", out object controller) && !string.IsNullOrWhiteSpace(controller?.ToString()))
                    log.Controller = controller.ToString();

                if (routeData.Values.TryGetValue("action", out object action) && !string.IsNullOrWhiteSpace(action?.ToString()))
                    log.Action = action.ToString();

                try
                {
                    await _next(httpContext);
                }
                catch (Exception ex)
                {
                    Exception? inner = ex.InnerException;

                    throw;
                }

                log.StatusCode = httpContext.Response.StatusCode;
                log.ResponseTime = DateTime.UtcNow.AddHours(4);

                if (httpContext.User.Identity.IsAuthenticated)
                {
                    int? userId = httpContext.User.GetUserId();
                    if (userId != null && userId > 0)
                        log.CreatedByUserId = userId;
                }

                await db.AuditLogs.AddAsync(log);
                await db.SaveChangesAsync();
            }
        }
    }

    public static class AuditMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuditMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuditMiddleware>();
        }
    }
}
