using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace RyanP410.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static bool IsValid(this IActionContextAccessor ctx)
        {
            return ctx.ActionContext.ModelState.IsValid;
        }
    }
}
