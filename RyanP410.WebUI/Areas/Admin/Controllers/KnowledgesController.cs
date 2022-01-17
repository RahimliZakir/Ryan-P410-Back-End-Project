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
using RyanP410.WebUI.AppCode.Modules.KnowledgesModule;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KnowledgesController : Controller
    {
        readonly IMediator mediator;

        public KnowledgesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            KnowledgesQuery query = new KnowledgesQuery();

            IEnumerable<Knowledge> data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Details(KnowledgeSingleQuery query)
        {
            Knowledge knowledge = await mediator.Send(query);

            if (knowledge == null)
            {
                return NotFound();
            }

            return View(knowledge);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KnowledgeCreateCommand request)
        {
            int id = await mediator.Send(request);

            if (id > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(KnowledgeSingleQuery query)
        {
            Knowledge knowledge = await mediator.Send(query);

            if (knowledge == null)
            {
                return NotFound();
            }

            var vm = new KnowledgeViewModel();

            vm.Id = knowledge.Id;
            vm.Name = knowledge.Name;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KnowledgeEditCommand request)
        {
            int identifier = await mediator.Send(request);

            if (identifier > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(KnowledgeRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
