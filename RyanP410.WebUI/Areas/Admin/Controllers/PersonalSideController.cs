using MediatR;
using Microsoft.AspNetCore.Mvc;
using RyanP410.WebUI.AppCode.Modules.PersonalSideModule;
using RyanP410.WebUI.Models.Entities.Membership;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PersonalSideController : Controller
    {
        readonly IMediator mediator;

        public PersonalSideController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async public Task<IActionResult> Index()
        {
            PersonalSideGetCurrentQuery query = new PersonalSideGetCurrentQuery();

            RyanUser currentUser = await mediator.Send(query);

            return View(currentUser);
        }

        async public Task<IActionResult> Edit()
        {
            PersonalSideGetCurrentQuery query = new PersonalSideGetCurrentQuery();

            RyanUser currentUser = await mediator.Send(query);

            return View(currentUser);
        }

        [HttpPost]
        async public Task<IActionResult> Edit(PersonalSideConfigureQuery request)
        {

            int id = await mediator.Send(request);

            if (id > 0)
                return RedirectToAction(nameof(Index), "PersonalSide");

            return View(request);
        }
    }
}
