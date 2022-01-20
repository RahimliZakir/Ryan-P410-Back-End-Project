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
using RyanP410.WebUI.AppCode.Modules.BlogCategoriesModule;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogCategoriesController : Controller
    {
        readonly IMediator mediator;

        public BlogCategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            BlogCategoriesQuery query = new BlogCategoriesQuery();

            IEnumerable<BlogCategory> data = await mediator.Send(query);

            return View(data);
        }

        public async Task<IActionResult> Details(BlogCategorySingleQuery query)
        {
            BlogCategory blogCategory = await mediator.Send(query);

            if (blogCategory == null)
            {
                return NotFound();
            }

            return View(blogCategory);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCategoryCreateCommand request)
        {
            int id = await mediator.Send(request);

            if (id > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        public async Task<IActionResult> Edit(BlogCategorySingleQuery query)
        {
            BlogCategory blogCategory = await mediator.Send(query);

            if (blogCategory == null)
            {
                return NotFound();
            }

            var vm = new BlogCategoryViewModel();

            vm.Id = blogCategory.Id;
            vm.Name = blogCategory.Name;

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogCategoryEditCommand request)
        {
            int identifier = await mediator.Send(request);

            if (identifier > 0)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BlogCategoryRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
