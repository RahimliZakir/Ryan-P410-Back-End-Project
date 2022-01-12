using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace RyanP410.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static bool IsValid(this IActionContextAccessor ctx)
        {
            return ctx.ActionContext.ModelState.IsValid;
        }

        public static void AddModelError(this IActionContextAccessor ctx, string key, string exception)
        {
            ctx.ActionContext.ModelState.AddModelError(key, exception);
        }
    }
}
