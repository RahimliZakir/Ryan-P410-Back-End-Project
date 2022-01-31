using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RyanP410.WebUI.AppCode.Modules.BlogsModule;

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

        async public Task<IActionResult> Details()
        {

            return View();
        }
    }
}
