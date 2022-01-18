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
using RyanP410.WebUI.AppCode.Modules.CategoriesModule;
using RyanP410.WebUI.AppCode.Modules.WorksModule;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorksController : Controller
    {
        readonly IMediator mediator;

        public WorksController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            WorksQuery query = new WorksQuery();

            IEnumerable<Work> data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Details(WorkSingleQuery query)
        {
            Work work = await mediator.Send(query);

            if (work == null)
            {
                return NotFound();
            }

            return View(work);
        }

        async public Task<IActionResult> Create()
        {
            CategoriesQuery query = new();
            var categories = await mediator.Send(query);

            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,CategoryId,File")] WorkCreateCommand request)
        {
            int id = await mediator.Send(request);

            if (id > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(WorkSingleQuery query)
        {
            Work work = await mediator.Send(query);

            if (work == null)
            {
                return NotFound();
            }

            var vm = new WorkViewModel();

            vm.Id = work.Id;
            vm.Title = work.Title;
            vm.CategoryId = work.CategoryId;
            vm.ImagePath = work.ImagePath;

            CategoriesQuery categoriesQuery = new();
            var categories = await mediator.Send(categoriesQuery);

            ViewBag.Categories = new SelectList(categories, "Id", "Name", work.CategoryId);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,CategoryId,File,FileTemp,Id")] WorkEditCommand request)
        {
            int identifier = await mediator.Send(request);

            if (identifier > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(WorkRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
