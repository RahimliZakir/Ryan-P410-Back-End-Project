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
using RyanP410.WebUI.AppCode.Modules.EducationsModule;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationsController : Controller
    {
        readonly IMediator mediator;

        public EducationsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            EducationsQuery query = new EducationsQuery();

            IEnumerable<Education> data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Details(EducationSingleQuery query)
        {
            Education education = await mediator.Send(query);

            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EducationCreateCommand request)
        {
            int id = await mediator.Send(request);

            if (id > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(EducationSingleQuery query)
        {
            Education education = await mediator.Send(query);

            if (education == null)
            {
                return NotFound();
            }

            var vm = new EducationViewModel();

            vm.Id = education.Id;
            vm.Position = education.Position;
            vm.Description = education.Description;
            vm.BeginYear = education.BeginYear;
            vm.EndYear = education.EndYear;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EducationEditCommand request)
        {
            int identifier = await mediator.Send(request);

            if (identifier > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EducationRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
