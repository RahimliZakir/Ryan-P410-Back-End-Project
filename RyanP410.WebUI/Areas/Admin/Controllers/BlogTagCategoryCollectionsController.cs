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
using RyanP410.WebUI.AppCode.Modules.BlogsModule;
using RyanP410.WebUI.AppCode.Modules.BlogTagCategoriesModule;
using RyanP410.WebUI.AppCode.Modules.TagsModule;
using RyanP410.WebUI.Areas.Admin.Models.FormModels;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogTagCategoryCollectionsController : Controller
    {
        readonly IMediator mediator;

        public BlogTagCategoryCollectionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async public Task<IActionResult> Index()
        {
            var query = new BlogTagCategoriesQuery();

            IEnumerable<Blog> data = await mediator.Send(query);

            return View(data);
        }

        async public Task<IActionResult> Details(BlogTagCategorySingleQuery query)
        {
            var collection = await mediator.Send(query);

            return View(collection);
        }

        public async Task<IActionResult> Create()
        {
            BlogsQuery blogsQuery = new();
            IEnumerable<Blog> blogs = await mediator.Send(blogsQuery);
            ViewBag.Blogs = new SelectList(blogs, "Id", "Title");

            TagsQuery tagsQuery = new();
            IEnumerable<Tag> tags = await mediator.Send(tagsQuery);
            ViewBag.Tags = new SelectList(tags, "Id", "Name");

            BlogCategoriesQuery blogCategoriesQuery = new();
            IEnumerable<BlogCategory> blogCategories = await mediator.Send(blogCategoriesQuery);
            ViewBag.BlogCategories = new SelectList(blogCategories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogTagCategoryCreateCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            if (!response.Error)
            {
                return Json(response);
            }

            return View(request);
        }

        async public Task<IActionResult> Edit(BlogTagCategorySingleQuery query)
        {
            BlogCollectionFormModel data = await mediator.Send(query);

            BlogsQuery blogsQuery = new();
            IEnumerable<Blog> blogs = await mediator.Send(blogsQuery);
            ViewBag.Blogs = new SelectList(blogs, "Id", "Title", data.Blog.Id);

            TagsQuery tagsQuery = new();
            IEnumerable<Tag> tags = await mediator.Send(tagsQuery);
            ViewBag.Tags = new SelectList(tags, "Id", "Name");

            BlogCategoriesQuery blogCategoriesQuery = new();
            IEnumerable<BlogCategory> blogCategories = await mediator.Send(blogCategoriesQuery);
            ViewBag.BlogCategories = new SelectList(blogCategories, "Id", "Name");

            return View(data);
        }

        [HttpPost]
        async public Task<IActionResult> Edit(BlogTagCategoryEditCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            if (response.Error == false)
                return Json(response);

            return View(request);
        }

        [HttpPost]
        async public Task<IActionResult> Delete(BlogTagCategoryRemoveCommand request)
        {
            JsonCommandResponse response = await mediator.Send(request);

            return Json(response);
        }
    }
}
