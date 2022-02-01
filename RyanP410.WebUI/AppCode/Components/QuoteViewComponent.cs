using MediatR;
using Microsoft.AspNetCore.Mvc;
using RyanP410.WebUI.AppCode.Modules.QuotesModule;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Components
{
    public class QuoteViewComponent : ViewComponent
    {
        async public Task<IViewComponentResult> InvokeAsync()
        {
            using (IServiceScope scope = HttpContext.RequestServices.CreateScope())
            {
                IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                QuoteSingleQuery query = new();


                Quote quote = await mediator.Send(query);

                return View(quote);
            }
        }
    }
}
