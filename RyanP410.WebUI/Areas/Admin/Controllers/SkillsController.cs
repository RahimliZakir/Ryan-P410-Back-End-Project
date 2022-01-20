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
using RyanP410.WebUI.AppCode.Modules.SkillsModule;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SkillsController : Controller
    {
        readonly IMediator mediator;

        public SkillsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            SkillsQuery query = new SkillsQuery();

            IEnumerable<Skill> data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Details(SkillSingleQuery query)
        {
            Skill skill = await mediator.Send(query);

            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] SkillCreateCommand request)
        {
            int id = await mediator.Send(request);

            if (id > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(SkillSingleQuery query)
        {
            var skill = await mediator.Send(query);

            if (skill == null)
            {
                return NotFound();
            }

            var vm = new SkillViewModel();

            vm.Id = skill.Id;
            vm.Name = skill.Name;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id")] SkillEditCommand request)
        {
            int identifier = await mediator.Send(request);

            if (identifier > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(SkillRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
