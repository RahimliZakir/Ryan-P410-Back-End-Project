using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RyanP410.WebUI.AppCode.Modules.BlogsModule;
using RyanP410.WebUI.AppCode.Modules.BlogTagCategoriesModule;
using RyanP410.WebUI.Models.Entities;
using RyanP410.WebUI.Models.ViewModels;

namespace RyanP410.WebUI.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        readonly IMediator mediator;

        public BlogController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async public Task<IActionResult> Index(BlogsPagedQuery pagedBlogsQuery)
        {
            var pagedBlogs = await mediator.Send(pagedBlogsQuery);

            return View(pagedBlogs);
        }

        async public Task<IActionResult> Details(BlogTagCategoryUserSideSingleQuery request)
        {
            BlogUserSideViewModel vm = new();

            Blog data = await mediator.Send(request);

            var blogPrevSingleQuery = new BlogSingleQuery((int)request.Id - 1);
            Blog prev = await mediator.Send(blogPrevSingleQuery);
            var blogNextSingleQuery = new BlogSingleQuery((int)request.Id + 1);
            Blog next = await mediator.Send(blogNextSingleQuery);

            vm.Blog = data;
            vm.PrevBlog = prev;
            vm.NextBlog = next;

            return View(vm);
        }
    }
}
