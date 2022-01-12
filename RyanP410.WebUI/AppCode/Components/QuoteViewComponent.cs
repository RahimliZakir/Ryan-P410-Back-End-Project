using MediatR;
using Microsoft.AspNetCore.Mvc;
using RyanP410.WebUI.AppCode.Modules.QuotesModule;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Components
{
    public class QuoteViewComponent : ViewComponent
    {
        readonly IMediator mediator;

        public QuoteViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async public Task<IViewComponentResult> InvokeAsync()
        {
            QuoteSingleQuery query = new();


            Quote quote = await mediator.Send(query);

            return View(quote);
        }
    }
}
