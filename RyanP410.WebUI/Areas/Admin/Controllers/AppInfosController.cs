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
using RyanP410.WebUI.AppCode.Modules.AppInfosModule;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppInfosController : Controller
    {
        readonly IMediator mediator;

        public AppInfosController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            AppInfosQuery query = new AppInfosQuery();

            IEnumerable<AppInfo> data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Details(AppInfoSingleQuery query)
        {
            AppInfo appInfo = await mediator.Send(query);

            if (appInfo == null)
            {
                return NotFound();
            }

            return View(appInfo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Map,Address,Email,PhoneNumber,IsFreelance")] AppInfoCreateCommand request)
        {
            int id = await mediator.Send(request);

            if (id > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(AppInfoSingleQuery query)
        {
            var appInfo = await mediator.Send(query);

            if (appInfo == null)
            {
                return NotFound();
            }

            var vm = new AppInfoViewModel();

            vm.Id = appInfo.Id;
            vm.Map = appInfo.Map;
            vm.Address = appInfo.Address;
            vm.Email = appInfo.Email;
            vm.PhoneNumber = appInfo.PhoneNumber;
            vm.IsFreelance = appInfo.IsFreelance;
            vm.CreatedDate = appInfo.CreatedDate;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Map,Address,Email,PhoneNumber,IsFreelance,Id")] AppInfoEditCommand request)
        {
            int identifier = await mediator.Send(request);

            if (identifier > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AppInfoRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
