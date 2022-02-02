using MediatR;
using RyanP410.WebUI.AppCode.Infrastructure;
using RyanP410.WebUI.Models.DataContexts;
using RyanP410.WebUI.Models.Entities;
using RyanP410.WebUI.Models.ViewModels;

namespace RyanP410.WebUI.AppCode.Modules.BlogsModule
{
    public class BlogsPagedQuery : IRequest<PagedViewModel<Blog>>
    {
        int pageIndex, pageSize;

        public int PageIndex
        {
            get
            {

                if (pageIndex > 0)
                {
                    return pageIndex;
                }

                return 1;
            }
            set
            {
                if (value > 0)
                {
                    pageIndex = value;
                }

                pageIndex = 1;
            }
        }

        public int PageSize
        {
            get
            {

                if (pageSize > 0)
                {
                    return pageSize;
                }

                return 4;
            }
            set
            {
                if (value > 0)
                {
                    pageSize = value;
                }

                pageSize = 4;
            }
        }

        public BlogsPagedQuery() { }

        public BlogsPagedQuery(int pageIndex = 1, int pageSize = 4)
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
