#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.AppCode.Modules.FunFactsModule;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FunFactsController : Controller
    {
        readonly IMediator mediator;

        public FunFactsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            FunFactsQuery query = new FunFactsQuery();

            IEnumerable<FunFact> data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Details(FunFactSingleQuery query)
        {
            FunFact funFact = await mediator.Send(query);

            if (funFact == null)
            {
                return NotFound();
            }

            return View(funFact);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Icon,Name")] FunFactCreateCommand request)
        {
            int id = await mediator.Send(request);

            if (id > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(FunFactSingleQuery query)
        {
            var funFact = await mediator.Send(query);

            if (funFact == null)
            {
                return NotFound();
            }

            var vm = new FunFactViewModel();

            vm.Id = funFact.Id;
            vm.Icon = funFact.Icon;
            vm.Name = funFact.Name;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Icon,Name,Id")] FunFactEditCommand request)
        {
            int identifier = await mediator.Send(request);

            if (identifier > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(FunFactRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
