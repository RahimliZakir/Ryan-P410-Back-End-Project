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
using RyanP410.WebUI.AppCode.Modules.ExperienceModule;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExperiencesController : Controller
    {
        readonly IMediator mediator;

        public ExperiencesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            ExperiencesQuery query = new ExperiencesQuery();

            IEnumerable<Experience> data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Details(ExperienceSingleQuery query)
        {
            Experience experience = await mediator.Send(query);

            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExperienceCreateCommand request)
        {
            int id = await mediator.Send(request);

            if (id > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(ExperienceSingleQuery query)
        {
            Experience experience = await mediator.Send(query);

            if (experience == null)
            {
                return NotFound();
            }

            var vm = new ExperienceViewModel();

            vm.Id = experience.Id;
            vm.Position = experience.Position;
            vm.Description = experience.Description;
            vm.BeginYear = experience.BeginYear;
            vm.EndYear = experience.EndYear;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ExperienceEditCommand request)
        {
            int identifier = await mediator.Send(request);

            if (identifier > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ExperienceRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
