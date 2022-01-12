using MediatR;
using Microsoft.AspNetCore.Mvc;
using RyanP410.WebUI.AppCode.Modules.AppInfosModule;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Components
{
    public class AppInfoViewComponent : ViewComponent
    {
        readonly IMediator mediator;

        public AppInfoViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async public Task<IViewComponentResult> InvokeAsync()
        {
            var query = new AppInfoSingleQuery();

            AppInfo? appInfo = await mediator.Send(query);

            return View(appInfo);
        }
    }
}
