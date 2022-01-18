using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RyanP410.WebUI.AppCode.Modules.CategoriesModule;
using RyanP410.WebUI.AppCode.Modules.WorksModule;
using RyanP410.WebUI.Models.Entities;
using RyanP410.WebUI.Models.ViewModels;

namespace RyanP410.WebUI.Controllers
{
    [AllowAnonymous]
    public class WorksController : Controller
    {
        readonly IMediator mediator;

        public WorksController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async public Task<IActionResult> Index()
        {
            WorksViewModel viewModel = new WorksViewModel();

            CategoriesQuery categoriesQuery = new();
            IEnumerable<Category> categories = await mediator.Send(categoriesQuery);
            viewModel.Categories = categories;

            WorksQuery worksQuery = new();
            IEnumerable<Work> works = await mediator.Send(worksQuery);
            viewModel.Works = works;

            return View(viewModel);
        }
    }
}
