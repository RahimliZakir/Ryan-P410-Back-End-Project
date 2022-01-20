#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.AppCode.Modules.PricingDetailsModule;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PricingDetailsController : Controller
    {
        readonly IMediator mediator;

        public PricingDetailsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            PricingDetailsQuery query = new PricingDetailsQuery();

            IEnumerable<PricingDetail> data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Details(PricingDetailSingleQuery query)
        {
            PricingDetail detail = await mediator.Send(query);

            if (detail == null)
            {
                return NotFound();
            }

            return View(detail);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] PricingDetailCreateCommand request)
        {
            int id = await mediator.Send(request);

            if (id > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(PricingDetailSingleQuery query)
        {
            var detail = await mediator.Send(query);

            if (detail == null)
            {
                return NotFound();
            }

            var vm = new PricingDetailViewModel();

            vm.Id = detail.Id;
            vm.Name = detail.Name;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id")] PricingDetailEditCommand request)
        {
            int identifier = await mediator.Send(request);

            if (identifier > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PricingDetailRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
