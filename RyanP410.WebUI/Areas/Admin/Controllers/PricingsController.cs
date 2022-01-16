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
using RyanP410.WebUI.AppCode.Modules.PricingsModule;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PricingsController : Controller
    {
        readonly IMediator mediator;

        public PricingsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            PricingsQuery query = new PricingsQuery();

            IEnumerable<Pricing> data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Details(PricingSingleQuery query)
        {
            Pricing pricing = await mediator.Send(query);

            if (pricing == null)
            {
                return NotFound();
            }

            return View(pricing);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Icon,Price,Per")] PricingCreateCommand request)
        {
            int id = await mediator.Send(request);

            if (id > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(PricingSingleQuery query)
        {
            var pricing = await mediator.Send(query);

            if (pricing == null)
            {
                return NotFound();
            }

            var vm = new PricingViewModel();

            vm.Id = pricing.Id;
            vm.Title = pricing.Title;
            vm.Icon = pricing.Icon;
            vm.Price = pricing.Price;
            vm.Per = pricing.Per;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,Icon,Price,Per,Id")] PricingEditCommand request)
        {
            int identifier = await mediator.Send(request);

            if (identifier > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PricingRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
