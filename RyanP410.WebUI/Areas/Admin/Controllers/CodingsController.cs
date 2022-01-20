#nullable disable
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.AppCode.Modules.CodingsModule;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CodingsController : Controller
    {
        readonly IMediator mediator;

        public CodingsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            CodingsQuery query = new CodingsQuery();

            IEnumerable<Coding> data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Details(CodingSingleQuery query)
        {
            Coding coding = await mediator.Send(query);

            if (coding == null)
            {
                return NotFound();
            }

            return View(coding);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CodingCreateCommand request)
        {
            int id = await mediator.Send(request);

            if (id > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(CodingSingleQuery query)
        {
            Coding coding = await mediator.Send(query);

            if (coding == null)
            {
                return NotFound();
            }

            var vm = new CodingViewModel();

            vm.Id = coding.Id;
            vm.Name = coding.Name;
            vm.Percent = coding.Percent;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CodingEditCommand request)
        {
            int identifier = await mediator.Send(request);

            if (identifier > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CodingRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
