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
using RyanP410.WebUI.AppCode.Modules.QuotesModule;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuotesController : Controller
    {
        readonly IMediator mediator;

        public QuotesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            QuotesQuery query = new QuotesQuery();

            IEnumerable<Quote> data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Details(QuoteSingleQuery query)
        {
            Quote quote = await mediator.Send(query);

            if (quote == null)
            {
                return NotFound();
            }

            return View(quote);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,Profession,Content,File")] QuoteCreateCommand request)
        {
            int id = await mediator.Send(request);

            if (id > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(QuoteSingleQuery query)
        {
            var quote = await mediator.Send(query);

            if (quote == null)
            {
                return NotFound();
            }

            var vm = new QuoteViewModel();

            vm.Id = quote.Id;
            vm.FullName = quote.FullName;
            vm.Profession = quote.Profession;
            vm.Content = quote.Content;
            vm.ImagePath = quote.ImagePath;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FullName,Profession,Content,File,Id,FileTemp")] QuoteEditCommand request)
        {
            int identifier = await mediator.Send(request);

            if (identifier > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(QuoteRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
