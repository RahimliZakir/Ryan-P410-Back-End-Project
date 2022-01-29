using MediatR;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using RyanP410.WebUI.Models.ViewModels;

namespace RyanP410.WebUI.AppCode.Modules.BlogsModule
{
    public class BlogsPagedQuery : PagenateTemplate, IRequest<PagedViewModel<Blog>>
    {
        public BlogsPagedQuery() { }

        public BlogsPagedQuery(int pageIndex = 1, int pageSize = 6)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        public class BlogsPagedQueryHandler : IRequestHandler<BlogsPagedQuery, PagedViewModel<Blog>>
        {
            readonly RyanDbContext db;

            public BlogsPagedQueryHandler(RyanDbContext db)
            {
                this.db = db;
            }

            async public Task<PagedViewModel<Blog>> Handle(BlogsPagedQuery request, CancellationToken cancellationToken)
            {
                var query = db.Blogs.OrderBy(b => b.Id);

                var vm = new PagedViewModel<Blog>(query, request.PageIndex, request.PageSize);

                return vm;
            }
        }
    }
}
