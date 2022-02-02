using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RyanP410.WebUI.AppCode.Dtos;
using RyanP410.WebUI.AppCode.Modules.BlogsModule;
using RyanP410.WebUI.AppCode.Modules.BlogTagCategoriesModule;
using RyanP410.WebUI.AppCode.Modules.CommentsModule;
using RyanP410.WebUI.Models.Entities;
using RyanP410.WebUI.Models.ViewModels;

namespace RyanP410.WebUI.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        readonly IMediator mediator;
        readonly IMapper mapper;

        public BlogController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
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
            BlogDetailsDto dto = mapper.Map<BlogDetailsDto>(data);

            BlogsQuery blogPrevSingleQuery = new();
            IEnumerable<Blog> prevBlogs = await mediator.Send(blogPrevSingleQuery);
            Blog prev = prevBlogs.Where(p => p.Id < data.Id).LastOrDefault();

            BlogsQuery blogNextSingleQuery = new();
            IEnumerable<Blog> nextBlogs = await mediator.Send(blogNextSingleQuery);
            Blog next = nextBlogs.Where(p => p.Id > data.Id).FirstOrDefault();

            vm.BlogDto = dto;
            vm.PrevBlog = prev;
            vm.NextBlog = next;

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        async public Task<IActionResult> PostComment(CommentCreateCommand request)
        {
            Comment data = await mediator.Send(request);

            if (data.ParentId.HasValue && data.ParentId > 0)
            {
                Response.Headers.Add("commentParentId", data.ParentId.Value.ToString());
            }

            return PartialView("_Comment", data);
        }
    }
}
