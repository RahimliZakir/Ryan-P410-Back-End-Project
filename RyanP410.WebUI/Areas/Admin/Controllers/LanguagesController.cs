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
using RyanP410.WebUI.AppCode.Modules.LanguagesModule;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LanguagesController : Controller
    {
        readonly IMediator mediator;

        public LanguagesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            LanguagesQuery query = new LanguagesQuery();

            IEnumerable<Language> data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Details(LanguageSingleQuery query)
        {
            Language language = await mediator.Send(query);

            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LanguageCreateCommand request)
        {
            int id = await mediator.Send(request);

            if (id > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(LanguageSingleQuery query)
        {
            Language language = await mediator.Send(query);

            if (language == null)
            {
                return NotFound();
            }

            var vm = new LanguageViewModel();

            vm.Id = language.Id;
            vm.Name = language.Name;
            vm.Percent = language.Percent;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LanguageEditCommand request)
        {
            int identifier = await mediator.Send(request);

            if (identifier > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LanguageRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
