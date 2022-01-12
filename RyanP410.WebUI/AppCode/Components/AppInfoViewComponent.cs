using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Components
{
    public class AppInfoViewComponent : ViewComponent
    {
        readonly RyanDbContext db;

        public AppInfoViewComponent(RyanDbContext db)
        {
            this.db = db;
        }

        async public Task<IViewComponentResult> InvokeAsync()
        {
            AppInfo? appInfo = await db.AppInfos.FirstOrDefaultAsync();

            return View(appInfo);
        }
    }
}
